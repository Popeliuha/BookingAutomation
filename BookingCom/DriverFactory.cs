using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace Framework
{
    public class DriverFactory
    {

        public Driver GetDriverByName(BrowserEnum browser)
        {
            IWebDriver driver;

            switch (browser)
            {
                case BrowserEnum.Headless:
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--headless"); 
                    driver = new ChromeDriver(options);
                    break;
                case BrowserEnum.Chrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserEnum.Firefox:
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
