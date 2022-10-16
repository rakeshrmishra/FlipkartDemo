using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using ReportingLibrary;
using System;
using System.Threading;
using Flipkart.Common;
using Flipkart.PageObjects;


namespace Flipkart.PageActions
{
		public class Login
	{
		IWebElement element = null;
		bool blnRetVal = false;
		Utils objUtils = new Utils();
		UIActions objUIA = new UIActions();
		LoginPageObject objLP = new LoginPageObject();
		HomePageObject hmp = new HomePageObject();
		ProductCategoryPageObject objpcp = new ProductCategoryPageObject();
		ProductSubCategoryPageObject objpscp = new ProductSubCategoryPageObject();
		ProductPageObject objpp = new ProductPageObject();
		private ExtentReportsHelper extentReportsHelper;
		
		public bool SignIn(UniversalDriver driver, ExtentTest extentTest, ExtentReportsHelper extent)
		{
			driver.Manage().Window.Maximize();
			blnRetVal = false;
			try
			{
				//Click on "X" button to close Sign In Dialog
				IWebElement CloseSignInDialog = objLP.CloseSignInDialog(driver, extent);
				Thread.Sleep(5000);
				objUIA.Click(driver, extentTest, extent, CloseSignInDialog, "X button on Sign In Dialog");
				Console.WriteLine("Sign In Dialog closed Sucessfully");

				//Click on Product Category (TVs & Appliances)
				IWebElement SelectProductCategory = hmp.ProductCategory(driver, extent);
				Thread.Sleep(5000);
				objUIA.Click(driver, extentTest, extent, SelectProductCategory, "on Mobiles & Tablets Category");
				Console.WriteLine("Product Category clicked Sucessfully");

				//Select Product sub category (
				Thread.Sleep(5000);
				IWebElement ProductSubCategory = objpcp.ProductSubCategory(driver, extent);
				objUIA.Click(driver, extentTest, extent, ProductSubCategory, "on TV & Appliances sub Category");
				Console.WriteLine("Product Sub category clicked Sucessfully");

				//Select Product
				Thread.Sleep(5000);
				IWebElement SelectProduct = objpscp.ProductPageCategory(driver, extent);
				objUIA.Click(driver, extentTest, extent, SelectProduct, "on Product");
				Console.WriteLine("Product selected Sucessfully");

				//Add to cart
				Thread.Sleep(5000);
				IWebElement AddtoCart = objpp.SelectProduct(driver, extent);
				objUIA.Click(driver, extentTest, extent, AddtoCart, "on Add to Cart");
				Console.WriteLine("Add to cart clicked Sucessfully");

				//Verify Cart product
				//Assert.AreEqual(entity.Id, user1.Id);
				Thread.Sleep(5000);
				string productname = driver.FindElement(By.XPath("//*[@id='container']/div/div[3]/div[1]/div[2]/div[2]/div/div[1]/h1/span")).GetAttribute("text");
				Console.WriteLine(productname);
				string productname_cart = driver.FindElement(By.XPath("//*[@id='container']/div/div[2]/div/div/div[1]/div/div[3]/div/div[1]/div[1]/div[1]/a")).GetAttribute("text");
				Console.WriteLine(productname_cart);
				Assert.AreEqual(productname, productname_cart);
				extent.SetStepStatusPass("Successfully Verified as : " + productname_cart);

			}
			catch (Exception ex)
			{
				objUtils.LogFailureAndTakeScreenshot(driver, extentTest, "Class: Login | Method: loginUsingSSO | Message: Some unhandled exception occured. Exception: " + ex.Message, "Exception");
			}
			return blnRetVal;

		}


	}
}