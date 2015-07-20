using System;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestContextExtension
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void setup()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            var extension = new Framework.TestContextExtension {Driver = new FirefoxDriver()};
            try
            {
                //extension = extension;
                //extension.Driver.Navigate().GoToUrl("http://accounts-dev.autodesk.com");
                extension.Driver.Navigate().GoToUrl("http://accounts-dev.autodesk.com");

                new PageOps(extension).Signin("testme", "Password1");

                extension.Driver.WaitForElement(new[] { "VerificationCode" });
                extension.Driver.FindElement("another method", 100).Click();
                extension.Driver.WaitForElement(new[] { "btnSubmit" });
            }
            finally
            {
                extension.Driver.Close();
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var extension = new Framework.TestContextExtension { Driver = new FirefoxDriver() };
            try
            {
                extension.Driver.Navigate().GoToUrl("http://accounts-dev.autodesk.com");

                new PageOps(extension).Signin("DeleteUserLoEKKsJtCbWjKs", "Password1");
                extension.Driver.WaitForElement(new[] { "view_profile_container" });

                extension.Driver.FindElement("security settings", 100).Click();
                extension.Driver.WaitForElement(new[] { "security_settings_container" });
            }
            finally
            {
                extension.Driver.Close();
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var extension = new Framework.TestContextExtension { Driver = new FirefoxDriver() };
            try
            {
                extension.Driver.Navigate().GoToUrl("http://accounts-dev.autodesk.com");

                new PageOps(extension).Signin("DeleteUserLoEKKsJtCbWjKs", "Password1");
                extension.Driver.WaitForElement(new[] { "view_profile_container" });

                extension.Driver.FindElement("linked accounts", 100).Click();
                extension.Driver.WaitForElement(new[] { "linked_accounts_container" });
            }
            finally
            {
                extension.Driver.Close();
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            var extension = new Framework.TestContextExtension { Driver = new FirefoxDriver() };
            try
            {
                extension.Driver.Navigate().GoToUrl("http://accounts-dev.autodesk.com");

                new PageOps(extension).Signin("DeleteUserLoEKKsJtCbWjKs", "Password1");
                extension.Driver.WaitForElement(new[] { "view_profile_container" });

                extension.Driver.FindElement("preferences", 100).Click();
                extension.Driver.WaitForElement(new[] { "change_language_btn" });
            }
            finally
            {
                extension.Driver.Close();
            }
        }
    }
}
