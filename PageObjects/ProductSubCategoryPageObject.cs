using OpenQA.Selenium;
using ReportingLibrary;
using System;
using System.Collections.Generic;
using Flipkart.Common;

namespace Flipkart.PageObjects
{
	class ProductSubCategoryPageObject
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
		public IWebElement ProductPageCategory(UniversalDriver driver, ExtentReportsHelper extent)
		{
			element = null;
			try
			{
				//element = utils.FindElementByLocator(driver, LoginScreenMap["Add_To_Cart"]);
				element = utils.FindElementByLocator(driver, "xPath+//*[@id='container']/div/div[3]/div/div[2]/div[2]/div/div/div/a/div[2]/div[1]/div[1]");
			}
			catch (Exception ex)
			{
				utils.Log.Error("Class: LoginPageObjects | Method: loginButton | Exception desc : " + ex.Message);
			}
			return element;
		}
	}
}

