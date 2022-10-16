using AventStack.ExtentReports;
using System;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using Flipkart.Common;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SeleniumExtras.WaitHelpers;

namespace Flipkart
{
	public class Utils
	{
		public TwicLogger Log
			= new TwicLogger();
		public static ExtentReports reports_IE = null;
		public static ExtentReports reports_Chrome = null;
		public static ExtentReports reports_FireFox = null;
		public static ExtentReports reports_Edge = null;
		public enum LocatorType { xpath, id, name, linktext, partiallinktext, classname, cssselector, tagname, none };
		public enum ExpectedCondition { visibility, presence, clickable };
		public enum Alert { Accept, Dismiss, GetText };
		public enum ElementStatus { Enabled, NotEnabled, Visible, NotVisible, Selected, NotSelected, ReadOnly };
		public enum RandomValueType { AlphaNumeric, Numeric, Alphabets, NumericWithoutZero };
		public enum DateCompare { Date1GtDate2, Date1LTDate2, Date1EQDate2 };
		public enum EnumLogStatus { PASS, INFO, WARNING, FAIL, HYPERLINK };
		private readonly JSWaiter jsWaiter = new JSWaiter();
		//private DesiredCapabilities desiredCapabilities;

		public String GetRandomVal()
		{
			String strRamdom = "";
			try
			{
				strRamdom = DateTime.Now.ToString("yyyyMMdd_hhmmsss");
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils | Method: getRandomVal | Exception desc : " + ex.Message);
			}
			return strRamdom;
		}

		public String TakeScreenShot(IWebDriver driver, String strFileName)
		{
			String strRetVal = "";
			try
			{
				Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot(); //As(OutputType.FILE);
				strFileName = strFileName + "_" + GetRandomVal() + ".jpeg";
				strRetVal = "./" + "Screenshots/" + strFileName;
				scrFile.SaveAsFile(Constants.strReportFilePath + "\\Screenshots\\" + strFileName);
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils | Method: takeScreenShot | Exception desc : " + ex.Message);
			}
			return strRetVal;
		}


		public String GetParentHandle(IWebDriver driver)
		{
			return driver.WindowHandles.First();
		}


		public IWebDriver InitDriver(String browserName)
		{
			IWebDriver driver = null;
			try
			{
				//desiredCapabilities.Platform = Platform.CurrentPlatform;
				switch (browserName.ToLower())
				{
					case "chrome":
						driver = new ChromeDriver();
						break;
					case "firefox":
						driver = new FirefoxDriver();
						break;
					case "edge":
						driver = new EdgeDriver();
						break;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils | Method: initDriver | Exception desc : " + ex.Message);
			}
			return driver;
		}



		public void LogFailureAndTakeScreenshot(IWebDriver driver, ExtentTest tempLogger, String failureDescription, String screenshotName)
		{
			try
			{
				Log.Error(failureDescription);
				tempLogger.Log(Status.Fail, "<b style=\"color:Red\">" + failureDescription + "</b>");
				tempLogger.Fail(screenshotName, MediaEntityBuilder.CreateScreenCaptureFromPath(TakeScreenShot(driver, screenshotName)).Build());
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils | Method: logFailureAndTakeScreenshot | Exception desc : " + ex.Message);
			}
		}
		public void ReportLogger(ExtentTest tempLogger, EnumLogStatus enumLogStatus, String strlog)
		{
			try
			{
				switch (enumLogStatus)
				{
					case EnumLogStatus.PASS:
						Log.Info(strlog);
						tempLogger.Log(Status.Pass, strlog);
						break;
					case EnumLogStatus.INFO:
						Log.Info(strlog);
						tempLogger.Log(Status.Info, strlog);
						break;
					case EnumLogStatus.WARNING:
						Log.Warn(strlog);
						tempLogger.Log(Status.Warning, strlog);
						break;
					case EnumLogStatus.FAIL:
						Log.Error(strlog);
						tempLogger.Log(Status.Fail, strlog);
						break;
					case EnumLogStatus.HYPERLINK:
						Log.Info(strlog);
						tempLogger.Log(Status.Pass, "Please click on <a href='" + "./" + "DownloadFiles/" + strlog + "'>" + strlog + "</a> to view");
						break;
				}
			}
			catch (Exception ex)
			{
				Log.Info("Class: Utils | Method: reportLogStatus | Exception desc : " + ex.Message);
			}
		}


		public void CleanDriverInstances()
		{
			KillProcess("chromedriver");
			KillProcess("IEDriverServer");
		}
		public void KillProcess(String strDriverName)
		{
			try
			{
				foreach (var p in Process.GetProcessesByName(strDriverName))
				{
					p.Kill();
				}

				Log.Info("Killed all the " + strDriverName + "  Instances");
			}
			catch (Exception ex)
			{
				Log.Fatal("Class: Utils | Method: killDriver | Exception desc : " + ex.Message);
			}
		}
		public LocatorType ExtractLocatorType(String strLocatorTypeAndValue)
		{
			LocatorType locType = LocatorType.none;
			try
			{
				String strLocator = strLocatorTypeAndValue.Split('=')[0];
				switch (strLocator.ToLower())
				{
					case "xpath":
						locType = LocatorType.xpath;
						break;
					case "id":
						locType = LocatorType.id;
						break;
					case "name":
						locType = LocatorType.name;
						break;
					case "linktext":
						locType = LocatorType.linktext;
						break;
					case "partiallinktext":
						locType = LocatorType.partiallinktext;
						break;
					case "classname":
						locType = LocatorType.classname;
						break;
					case "cssselector":
						locType = LocatorType.cssselector;
						break;
					case "tagname":
						locType = LocatorType.tagname;
						break;
				}
			}
			catch (Exception ex)
			{
				Log.Fatal("Class: Utils | Method: extractLocatorType| Exception desc : " + ex.Message);
			}
			return locType;
		}

		public String ExtractLocatorValue(String strLocatorTypeAndValue)
		{
			String strRetVal = null;
			try
			{
				strRetVal = strLocatorTypeAndValue.Split('=')[1];
			}
			catch (Exception ex)
			{
				Log.Fatal("Class: Utils | Method: extractLocatorValue| Exception desc : " + ex.Message);
			}
			return strRetVal;
		}

		public IWebElement FindElementByLocator(UniversalDriver driver, String strLocatorAndXpath)
		{
			IWebElement element = null;
			try
			{
				char[] delimiter = { '+' };
				String[] strArray = strLocatorAndXpath.Split(delimiter);
				element = ProcessWebElementBasedOnLocator(driver, strArray);
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils | Method: findElementByLocator| Exception desc : " + ex.Message);
			}
			return element;
		}

		public IWebElement ProcessWebElementBasedOnLocator(UniversalDriver driver, String[] strArray)
		{
			IWebElement element = null;
			String strLocator = strArray[0];
			String strLocatorValue = strArray[1];
			try
			{
				switch (strLocator.ToLower())
				{
					case "xpath":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.XPath(strLocatorValue)));
						element = driver.FindElement(By.XPath(strLocatorValue));
						break;
					case "id":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.Id(strLocatorValue)));
						element = driver.FindElementById(strLocatorValue);
						break;
					case "name":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.Name(strLocatorValue)));
						element = driver.FindElement(By.Name(strLocatorValue));
						break;
					case "linktext":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.LinkText(strLocatorValue)));
						element = driver.FindElement(By.LinkText(strLocatorValue));
						break;
					case "partiallinktext":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.PartialLinkText(strLocatorValue)));
						element = driver.FindElement(By.PartialLinkText(strLocatorValue));
						break;
					case "classname":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.ClassName(strLocatorValue)));
						element = driver.FindElement(By.ClassName(strLocatorValue));
						break;
					case "cssselector":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.CssSelector(strLocatorValue)));
						element = driver.FindElement(By.CssSelector(strLocatorValue));
						break;
					case "tagname":
						new WebDriverWait(driver, new TimeSpan(30)).Until(ExpectedConditions.ElementExists(By.TagName(strLocatorValue)));
						element = driver.FindElement(By.TagName(strLocatorValue));
						break;
				}
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils| Method: processWebElementBasedOnLocator | Exception desc : " + ex.Message);
			}
			return element;
		}

		public Boolean WaitForAnElement(IWebDriver driver, String LocatorTypeAndValue, ExpectedCondition elementCondition, long ts)
		{
			Boolean blnRetVal = false;
			TimeSpan timeSpanToWait = new TimeSpan(ts);
			try
			{
				/*there will be three expected conditions we are handling here
				Clickable, Presence and Visibility
				*/

				LocatorType locatorType = ExtractLocatorType(LocatorTypeAndValue);
				String locatorValue = ExtractLocatorValue(LocatorTypeAndValue);


				switch (elementCondition)
				{
					case ExpectedCondition.clickable:
						new WebDriverWait(driver, timeSpanToWait).Until(ExpectedConditions.ElementToBeClickable(GetElementLocatorBy(locatorType, locatorValue)));
						break;
					case ExpectedCondition.presence:
						new WebDriverWait(driver, timeSpanToWait).Until(ExpectedConditions.ElementExists(GetElementLocatorBy(locatorType, locatorValue)));
						break;
					case ExpectedCondition.visibility:
						new WebDriverWait(driver, timeSpanToWait).Until(ExpectedConditions.ElementIsVisible(GetElementLocatorBy(locatorType, locatorValue)));
						break;

				}

				blnRetVal = true;
			}
			catch (Exception ex)
			{
				Log.Error("Class: Utils| Method: waitForAnElement | Exception desc : " + ex.Message);
			}
			return blnRetVal;
		}
		
		private By GetElementLocatorBy(LocatorType locator, String locatorValue)
		{
			By objBy = null;
			switch (locator)
			{
				case LocatorType.id:
					objBy = By.Id(locatorValue);
					break;
				case LocatorType.xpath:
					objBy = By.XPath(locatorValue);
					break;
				case LocatorType.classname:
					objBy = By.ClassName(locatorValue);
					break;
				case LocatorType.cssselector:
					objBy = By.CssSelector(locatorValue);
					break;
				case LocatorType.linktext:
					objBy = By.LinkText(locatorValue);
					break;
				case LocatorType.name:
					objBy = By.Name(locatorValue);
					break;
				case LocatorType.partiallinktext:
					objBy = By.PartialLinkText(locatorValue);
					break;
				case LocatorType.tagname:
					objBy = By.TagName(locatorValue);
					break;
			}
			return objBy;
		}
	}
}
