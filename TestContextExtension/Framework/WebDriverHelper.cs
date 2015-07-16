using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Framework
{
    public static class WebDriverHelper
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeout)
        {
            IList<IWebElement> founds = null;
            if (timeout > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                Thread.Sleep(3000);
                try
                {
                    founds = wait.Until(drv => drv.FindElements(by));
                    if (founds != null && founds.Count == 1)
                    {
                        return founds[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
            return driver.FindElement(by);
        }

        public static void WaitForIframe(this IWebDriver driver, int timeout)
        {
            WaitForElement(driver, new[] { "iframe" }, timeout);
        }

        public static IWebElement FindElement(this IWebDriver driver, string[] bys, int locator, int timeout)
        {
            IWebElement find = null;
            if (bys.Length > 0)
            {
                if (bys.Any(@by => (find = FindElement(driver, @by, locator, timeout)) != null))
                {
                    return find;
                }
            }
            return null;
        }


        public static IWebElement FindElement(this IWebDriver driver, string by, int locator, int timeout)
        {

            IList<IWebElement> founds = new List<IWebElement>();
            if (timeout > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                Type findBy = typeof(By);
                MethodInfo[] finds = findBy.GetMethods(BindingFlags.Public | BindingFlags.Static);

                foreach (var methodInfo in finds)
                {
                    if (methodInfo.ReturnType == typeof(By))
                    {
                        try
                        {
                            By currBy = null;

                            try
                            {
                                currBy = (By)methodInfo.Invoke(null, new object[] { by });
                            }
                            catch (TargetInvocationException)
                            {
                                continue;
                            }

                            founds = wait.Until(drv =>
                            {
                                try
                                {
                                    return drv.FindElements(currBy);
                                }
                                catch (NoSuchElementException)
                                {
                                    return new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(new List<IWebElement>());
                                }
                            });
                        }
                        catch
                        {
                            continue;
                        }
                        if (founds.Count == 0)
                            continue;

                        if (founds != null && founds.Count > 0)
                        {
                            return founds[locator];
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 1. Id, 2, Name, 3, xpath, 4 classname, 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver driver, string by, int timeout)
        {
            return FindElement(driver, by, 0, timeout);
        }

        public static void WaitForElement(this IWebDriver driver, string[] bys, int timeout = 100, int locator = 0)
        {
            IWebElement element = FindElement(driver, bys, locator, timeout);

            if (element == null || !element.Displayed)
            {
                do
                {
                    Thread.Sleep(1000);
                    element = FindElement(driver, bys, locator, timeout);
                    if (element != null)
                    {
                        if (element.Displayed)
                        {
                            break;
                        }
                    }
                    timeout--;
                } while (timeout >= 0);
            }
        }

        public static void WaitForError(this IWebDriver driver, string className = "field-validation-error")
        {
            Trace.WriteLine("Checking for error control");
            var timeout = 100;
            Thread.Sleep(1000);
            IList<IWebElement> element = driver.FindElements(By.ClassName(className));

            if (element.Count <= 0)
            {
                do
                {
                    Thread.Sleep(1000);
                    element = driver.FindElements(By.ClassName(className));
                    timeout--;
                } while (element.Count <= 0 || timeout <= 0);
            }
        }

        public static List<string> GetErrorMessages(this IWebDriver driver, string className = "field-validation-error")
        {
            IList<IWebElement> elements = driver.FindElements(By.ClassName(className));

            return elements.Select(item => item.Text).ToList();
        }

        public static void WaitForElementExit(this IWebDriver driver, string[] bys, int timeout = 100, int locator = 0)
        {
            IWebElement element = FindElement(driver, bys, locator, timeout);

            if (element != null)
            {
                if (element.Displayed)
                {
                    do
                    {
                        Thread.Sleep(1000);
                        element = FindElement(driver, bys, locator, timeout);
                        timeout--;
                    } while (!element.Displayed && timeout <= 0);
                }
            }
        }

        public static void WaitForIFramePopUp(this IWebDriver driver, int timeout = 100)
        {
            List<IWebElement> elements = driver.FindElements(By.TagName("iframe")).ToList();

            if (elements.Count >= 1)
            {
                do
                {
                    Thread.Sleep(1000);
                    elements = driver.FindElements(By.TagName("iframe")).ToList();
                    timeout--;
                } while (!(elements.Count > 1) && timeout <= 0);
            }
        }

        public static void WaitForOtherWinodowsDialog(this IWebDriver driver, int timeout = 100)
        {
            var elements = driver.WindowHandles.ToList();

            if (elements.Count >= 1)
            {
                do
                {
                    Thread.Sleep(1000);
                    elements = driver.WindowHandles.ToList();
                    timeout--;
                } while (!(elements.Count > 1) && timeout <= 0);
            }
        }

        public static void WaitForControlTextChange(this IWebElement element, string oldText, string newText)
        {
            Trace.WriteLine("Checking whether the text on the control has changed");
            var timeout = 100;
            Thread.Sleep(1000);

            for (var i = 0; i < 10; i++)
            {
                if (element.Text.Equals(newText))
                {
                    break;
                }
                if (!element.Text.Equals(oldText)) continue;
                Thread.Sleep(100);
            }
        }

        public static void WaitForControlToEnable(this IWebElement element, int timeout = 100)
        {
            Trace.WriteLine("Checking whether control is enabled");

            do
            {
                Thread.Sleep(1000);
                timeout--;
            } while (!(element.Enabled));
        }


        public static void WaitForNotification(this IWebDriver driver, int timeout = 100)
        {
            driver.WaitForElement(new[] { "alert_container" }, timeout);
        }


        public static void WaitForControlToDisappear(this IWebElement element, int timeout = 100)
        {
            Trace.WriteLine("Checking whether control is not visible");

            do
            {
                Thread.Sleep(1000);
                timeout--;
            } while ((element.Displayed));
        }

    }
}
