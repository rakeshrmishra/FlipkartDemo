using OpenQA.Selenium;
using ReportingLibrary;
using System;
using System.Collections.Generic;
using Flipkart.Common;
using System.Threading;

namespace Flipkart.PageObjects
{
	class ProductPageObject
	{
		Utils utils = new Utils();
		private IWebElement element = null;
		public static Dictionary<string, string> LoginScreenMap = new Dictionary<string, string>();

		public String GetLocaterTypeAndValue(String strName)
		{
			if (LoginScreenMap.ContainsKey(strName))
				return LoginScreenMap[strName];
			else
				return null;
		}
		public IWebElement SelectProduct(UniversalDriver driver, ExtentReportsHelper extent)
		{
			//switch to second tab
			driver.SwitchTo().Window(driver.WindowHandles[1]);
			//get current window handle id
			Console.WriteLine("Current window id: " + driver.CurrentWindowHandle);
			Console.WriteLine("Page title in second tab is: " + driver.Title);
			Thread.Sleep(15000);
			element = null;
			try
			{
				element = utils.FindElementByLocator(driver, LoginScreenMap["Select_Product"]);
			}
			catch (Exception ex)
			{
				utils.Log.Error("Class: LoginPageObjects | Method: loginButton | Exception desc : " + ex.Message);
			}
			return element;
		}
	}
}

