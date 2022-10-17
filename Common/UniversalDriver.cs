using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flipkart.Common
{
    public class UniversalDriver : IWebDriver
    {
        private IWebDriver Driver { get; set; }

        public UniversalDriver(TestContext context)
        {
            var platform = context.Properties["Platform"];

            Driver = new ChromeDriver { Url = "https://flipkart.com" };
        }

        public string Url
        {
            get
            {
                return Driver.Url;
            }
            set
            {
                Driver.Url = value;
            }
        }

        public string Title => Driver.Title;

        public string PageSource => Driver.PageSource;

        public string CurrentWindowHandle => Driver.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => Driver.WindowHandles;

        public IWebDriver getDriver => Driver;

        public object TimeSpan { get; internal set; }

        public void Close()
        {
            Driver.Close();
        }

        public IWebElement FindElementById(string id)
        {
            return FindElementByIdInternal(id);

        }

        private IWebElement FindElementByIdInternal(string id, int depth = 0)
        {
            IWebElement element;
            try
            {
                element = Driver.FindElement(By.Id(id));
            }

            catch (NoSuchElementException)
            {
                if (depth > 8)
                {
                    throw;
                }

                var timeout = (int)Math.Pow(4, depth);
                Thread.Sleep(timeout);

                element = FindElementByIdInternal(id, depth + 1);
            }

            return element;
        }

        public IWebElement FindElement(By by)
        {
            return Driver.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Driver.FindElements(by);
        }

        public IOptions Manage()
        {
            return Driver.Manage();
        }

        public INavigation Navigate()
        {
            return Driver.Navigate();
        }

        public void Quit()
        {
            Driver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return Driver.SwitchTo();
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
