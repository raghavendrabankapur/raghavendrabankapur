using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework
{
    internal abstract class Page
    {
        private const int frequency = 10;

        protected TestContextExtension Extension { get; set; }

        protected IDictionary<string, Lazy<IWebElement>> lazyElements = new Dictionary<string, Lazy<IWebElement>>();

        protected Page(TestContextExtension extesion)
        {
            Extension = extesion;
            int tryFrequency = 0;
            bool exceptionCaught;
            do
            {
                exceptionCaught = false;

                try
                {
                    this.WaitForPageReady();
                    this.InitPage();
                }
                catch (Exception e)
                {
                    if (tryFrequency > frequency)
                    {
                        Trace.WriteLine(e.StackTrace);
                        Assert.Fail(e.Message);
                    }

                    exceptionCaught = true;
                    tryFrequency++;
                }
            } while (exceptionCaught);
        }

        protected Page()
        {
            int tryFrequency = 0;
            bool exceptionCaught;
            do
            {
                exceptionCaught = false;

                try
                {
                    this.WaitForPageReady();
                    this.InitPage();
                }
                catch (Exception e)
                {
                    if (tryFrequency > frequency)
                    {
                        Trace.WriteLine(e.StackTrace);
                        Assert.Fail(e.Message);
                    }

                    exceptionCaught = true;
                    tryFrequency++;
                }
            } while (exceptionCaught);
        }

        protected Page(bool? extend)
        {
        }

        public virtual void InitPage()
        {
            IWebDriver webDriver = this.Extension.Driver;

            Type curr = this.GetType();

            PropertyInfo[] pros = curr.GetProperties();
            foreach (var pro in pros)
            {
                object[] attrs = pro.GetCustomAttributes(typeof (ElementDefinitionAttribute), true);
                if (attrs.Length == 1)
                {
                    var name = ((ElementDefinitionAttribute) attrs[0]).path;
                    var locator = ((ElementDefinitionAttribute) attrs[0]).locator;
                    var mode = ((ElementDefinitionAttribute) attrs[0]).mode;

                    IWebElement val = null;
                    Lazy<IWebElement> lazyVal = null;
                    if (mode == ElementMode.Lazy && !lazyElements.ContainsKey(pro.Name))
                    {
                        lazyVal = new Lazy<IWebElement>(() => webDriver.FindElement(name, locator, 3000));
                        lazyElements.Add(pro.Name, lazyVal);
                        continue;

                    }

                    if (mode != ElementMode.Lazy)
                    {
                        val = webDriver.FindElement(name, locator, 3000);
                        if (val == null && mode == ElementMode.Strict)
                            throw new Exception("can not find the element :" + pro.Name + " by the identifier - " + name);
                        //if (val == null)
                        pro.SetValue(this, val, null);
                    }
                }
            }
        }



        public abstract void WaitForPageReady();

        public string GetElementPathByPropertyName(string name, int pathLocator = 0)
        {
            Type curr = this.GetType();
            PropertyInfo prop = curr.GetProperty(name);
            object[] attrs = prop.GetCustomAttributes(typeof (ElementDefinitionAttribute), true);
            if (attrs.Length == 1)
            {
                return ((ElementDefinitionAttribute) attrs[0]).path[pathLocator];
            }
            return string.Empty;
        }
    }
}
