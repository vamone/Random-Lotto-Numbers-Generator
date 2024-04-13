using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottonRandomNumberGeneratorV2.Extensions
{
    public static class WebElementExtensions
    {
        public static IWebElement FindElementWithRetry(this IWebDriver driver, By by, int retryCount = 10, int sleep = 1000, bool isIngnoredIfNotFound = false)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    var element = driver.FindElement(by);
                    if (element != null)
                    {
                        Thread.Sleep(sleep);
                        return element;
                    }

                    Thread.Sleep(sleep);
                }
                catch (NoSuchElementException ex)
                {
                    if (isIngnoredIfNotFound)
                    {
                        return null;
                    }

                    Thread.Sleep(sleep);
                }
                catch(Exception ex)
                {
                    var abb = 1;
                }
            }

            throw new InvalidOperationException($"Cannot find element by '{by.ToString()}'.");
        }

        public static void JSClick(this IWebDriver driver, IWebElement element)
        {
            if(element == null)
            {
                return;
            }

            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}