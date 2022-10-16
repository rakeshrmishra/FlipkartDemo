using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace Flipkart.Common
{
    public class ExtentReportSetup
    {
        public static IWebDriver driver;
        protected ExtentReports _extent;
        public ExtentTest _test;

        ///For report directory creation and HTML report template creation
        ///For driver instantiation
        //[ClassInitialize]
        public ExtentReportSetup(TestContext testContext)
        {
            try
            {
                //To create report directory and add HTML report into it
                this.testContext = testContext;
                _extent = new ExtentReports();
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string currentTime = DateTime.Now.ToString("hhmmsstt");
                string reportPath = projectPath + "Results\\ExtentReport\\TestRunReport_" + currentTime + ".html";
                var htmlReporter = new ExtentHtmlReporter(reportPath);
                _extent.AddSystemInfo("Application Name", "TXWICMobile");
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }

            try
            {
            }
            catch (Exception e)

            {
            }
        }

        ///Getting the name of current running test to extent report
        TestContext testContext;
        [TestInitialize]
        public void BeforeTest()
        {
            try
            {
                _test = _extent.CreateTest(testContext.TestName);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// Finish the execution and logging the detials into HTML report
        [TestCleanup]
        public void AfterTest()
        {
            try
            {
                var status = testContext.CurrentTestOutcome;      
                Status logstatus;
                switch (status)
                {
                    case UnitTestOutcome.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, testContext.TestName);
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        _test.Log(logstatus, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case UnitTestOutcome.NotRunnable:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        ///To flush extent report
        ///To quit driver instance
        [ClassInitialize]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
            driver.Quit();
        }

        /// To capture the screenshot for extent report and return actual file path
        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string currentTime = DateTime.Now.ToString("hhmmsstt");
                string finalpth = projectPath + "Results\\Screenshots\\Screenshot_" + currentTime + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }
    }
}
