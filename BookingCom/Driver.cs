using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class Driver
    {
        public IWebDriver driver { get; set; }

        public Driver(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void MaximizeWindow()
        {
            driver.Manage().Window.Maximize();
        }

        public Element FindElementByXpath(string XPath)
        {
            return new Element (driver.FindElement(By.XPath(XPath)));
        }

        public Element FindElementByClass(string className)
        {
            return new Element(driver.FindElement(By.ClassName(className)));
        }

        public Element FindElementBytag(string tag)
        {
            return new Element(driver.FindElement(By.TagName(tag)));
        }

        public List<Element> FindElementsByXpath(string XPath)
        {
            var elements = driver.FindElements(By.XPath(XPath));
            var result = elements.Select(x => new Element(x));
            return result.ToList();
        }

        public void WaitUntilPageTitleContainsText(int secondsToWait, string titleText)
        {
            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
                driverWait.Until(ExpectedConditions.TitleContains(titleText));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException($"Page does not contain {titleText} in title, \n{e.StackTrace}, \n{e.Message}");
            }
        }

        public void WaitUntilElementExists(int secondsToWait, string webElementXPath)
        {
            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
                driverWait.Until(ExpectedConditions.ElementExists(By.XPath(webElementXPath)));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException($"Element did not appear in DOM, \n{e.StackTrace}, \n{e.Message}");
            }
        }

        public void WaitUntilElementDisappearsFromDOM(int secondsToWait, string webElementXPath)
        {
            try
            {
                WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
                driverWait.Until(d => driver.FindElements(By.XPath(webElementXPath)).ToList().Count == 0);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException($"Element did not disappear from DOM, \n{e.StackTrace}, \n{e.Message}");
            }
        }

        public void ExecuteJsCommand(string command)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(command);
        }

        public void CloseDriver()
        {
            driver.Close();
        }
    }
}
