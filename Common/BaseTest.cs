namespace Flipkart.Common
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Flipkart.PageObjects;
    using ReportingLibrary;
    using SeleniumHelperLibrary;
    using System;
    using Flipkart.PageObjects;

    [TestClass]
    public class BaseTest
    {
        protected static AventStack.ExtentReports.ExtentTest logger;
        protected static AventStack.ExtentReports.ExtentTest tempLogger;

        //#region Test Setup
        protected static UniversalDriver Driver { get; set; }
        protected static ExtentReportsHelper extent { get; set; }
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public void SetUpReporter()
        {

        }

        [TestInitialize]
        public void StartUpTest()
        {
            extent.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void AfterTest()
        {
            try
            {
                UnitTestOutcome status = TestContext.CurrentTestOutcome;
                switch (status)
                {
                    case UnitTestOutcome.Failed:
                        extent.SetTestStatusFail("Test Case failed");
                        extent.AddTestFailureScreenshot(Driver.getDriver.ScreenCaptureAsBase64String());
                        extent.Close();
                        Driver.Quit();
                        break;
                    case UnitTestOutcome.NotRunnable:
                        extent.SetTestStatusSkipped();
                        extent.Close();
                        Driver.Quit();
                        break;
                    default:
                        extent.AddTestFailureScreenshot(Driver.getDriver.ScreenCaptureAsBase64String());
                        extent.SetTestStatusPass();
                        extent.Close();
                        Driver.Quit();
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {

            }
        }

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Driver = new UniversalDriver(context);
            extent = new ExtentReportsHelper();
            PageObjectLoader loader = new PageObjectLoader();
            loader.LoadPageObjects();
        }

        [ClassCleanup]
        public void Cleanup()
        {
            extent.Close();
            Driver.Dispose();
        }
        //#endregion
    }
}