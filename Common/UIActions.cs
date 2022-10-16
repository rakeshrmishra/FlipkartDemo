using AventStack.ExtentReports;
using OpenQA.Selenium;
using ReportingLibrary;
using System;

namespace Flipkart.Common
{
	public class UIActions
	{

		bool blnRetVal = false;
		Utils ObjUtils = new Utils();
		public enum DropdownOptions { SelectByIndex, SelectByValue, SelectByVisibleText };
		public enum CheckboxOptions { Select, DeSelect };

		//Navigate to a Url
		public bool Navigate(IWebDriver driver, String url)
		{
			blnRetVal = false;
			try
			{
				driver.Url = url;
				driver.Navigate();
			}
			catch (Exception ex)
			{
				ObjUtils.Log.Fatal("Class: UIActions | Method: navigate | Exception desc : " + ex.Message);
			}
			return blnRetVal;
		}
		
		//Click a button or element
		public bool Click(UniversalDriver driver, ExtentTest TempLogger, ExtentReportsHelper extent, IWebElement element, String elementName)
		{
			blnRetVal = false;
			try
			{
				ObjUtils.Log.Info("Trying to locate " + elementName);
				if (element != null)
				{
					ObjUtils.Log.Info("Successfully located " + elementName);
					element.Click();
					extent.SetStepStatusPass("Successfully clicked " + elementName);
					ObjUtils.Log.Info("Successfully clicked " + elementName);
					blnRetVal = true;
				}
				else
				{
					extent.SetTestStatusFail("Unable to locate " + elementName);
				}
			}
			catch (Exception ex)
			{
			}
			blnRetVal = true;
			return blnRetVal;
		}
	}
}
