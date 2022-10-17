using OpenQA.Selenium;
using ReportingLibrary;
using System;
using System.Collections.Generic;
using Flipkart.Common;
using System.IO;

namespace Flipkart.PageObjects
{
	public class LoginPageObject
	{
		Utils utils = new Utils();
		private IWebElement element = null;
		public static Dictionary<string, string> LoginScreenMap = new Dictionary<string, string>();
		public string Close_SignIN { get; set; }
		
		public String GetLocaterTypeAndValue(String strName)
		{
			if (LoginScreenMap.ContainsKey(strName))
				return LoginScreenMap[strName];
			else
				return null;
		}

	
		public IWebElement CloseSignInDialog(UniversalDriver driver, ExtentReportsHelper extent)
		{
			element = null;
			try
			{
				element = utils.FindElementByLocator(driver, LoginScreenMap["Close_SignIN"]);
			}
			catch (Exception ex)
			{
				utils.Log.Error("Class: LoginPageObjects | Method: loginButton | Exception desc : " + ex.Message);
			}
			return element;
		}
	}
}
