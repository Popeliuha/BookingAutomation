using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework
{
    public class DriverFactory
    {
        //private readonly IObjectContainer _objectContainer;

        public Driver GetDriverByName(string browserName)
        {
            IWebDriver driver;

            switch (browserName)
            {
                case "headless":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--headless"); 
                    driver = new ChromeDriver(options);
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    string geckodriverPath = "C:/Git/geckodriver.exe";
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(geckodriverPath);
                    driver = new FirefoxDriver(service);
                    break;
                default:
                    throw new Exception("You selected wrong browser");
            }

            return new Driver(driver);
        }
    }
}
