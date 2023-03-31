﻿using OpenQA.Selenium;
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

        public void CloseDriver()
        {
            driver.Close();
        }
    }
}
