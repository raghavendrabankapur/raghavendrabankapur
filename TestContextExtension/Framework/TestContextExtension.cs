using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework
{
    [Serializable]
    public class TestContextExtension : TestContext
    {
        private IWebDriver driver;
        private  static  TestContextExtension testContextInstance;

        public TestContextExtension GetInstace
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public override void AddResultFile(string fileName)
        {
            testContextInstance.AddResultFile(fileName);
        }

        public override void BeginTimer(string timerName)
        {
            testContextInstance.BeginTimer(timerName);
        }

        public override System.Data.Common.DbConnection DataConnection
        {
            get { return testContextInstance.DataConnection; }
        }

        public override DataRow DataRow
        {
            get { return testContextInstance.DataRow; }
        }

        public override void EndTimer(string timerName)
        {
            testContextInstance.EndTimer(timerName);
        }

        public override System.Collections.IDictionary Properties
        {
            get { return testContextInstance.Properties; }
        }

        public override void WriteLine(string format, params object[] args)
        {
            testContextInstance.WriteLine(format, args);
        }

        public IWebDriver Driver
        {
            get { return driver; }
            set { driver = value; }
        }
    }
}
