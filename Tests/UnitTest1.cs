using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Flipkart.Common;
using Flipkart.PageActions;

namespace Flipkart
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        Utils objUtils = new Utils();
        Login objLogin = new Login();

        [TestMethod]

        public void CloseSignInDialog()
        {
            objUtils.Log.Info("Closing login Dialog");
            if (objLogin.SignIn(Driver, tempLogger, extent))
            {
                Console.WriteLine("Pass");
            }
        }
    }
}
