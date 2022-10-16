//SeleniumHelperLibrary.cs
using OpenQA.Selenium;
using System;

namespace SeleniumHelperLibrary
{
    public static class WebDiverExtensions
    {
        public static string ScreenCaptureAsBase64String(this IWebDriver driver)
        {
            /*ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
            */
            string localpath = "";
            try
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string currentTime = DateTime.Now.ToString("hhmmsstt");
                string finalpth = projectPath + "Results\\Screenshots\\Screenshot_" + currentTime + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
                return screenshot.AsBase64EncodedString;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}