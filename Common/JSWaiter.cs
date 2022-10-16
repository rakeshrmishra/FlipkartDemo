using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Flipkart.Common
{
    public class JSWaiter
    {
        private IWebDriver jsWaitDriver;
        private WebDriverWait jsWait;
        private IJavaScriptExecutor jsExec;

        public void SetDriver(IWebDriver webDriver)
        {
            jsWaitDriver = webDriver;
            jsWait = new WebDriverWait(webDriver, new TimeSpan(90));
            jsExec = (IJavaScriptExecutor)jsWaitDriver;
        }

    }
}
