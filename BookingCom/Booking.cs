using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace BookingCom
{
    public class Booking
    {
        IWebDriver driver;

        [SetUp]
        public void Init()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.booking.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test()
        {
            string city = "New York";
            CloseModalWindowIdAppears(driver);
            ChangeLaguageToEnglishUk(driver);
            SelectDestination(driver, city);
            FindAndClickCheckInCheckOut(driver);

            DateTime checkInDate = DateTime.Now.AddDays(1);
            string checkInMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkInDate.Month);
            string checkInDay = checkInDate.Day.ToString();

            DateTime checkOutDate = checkInDate.AddDays(7);
            string checkOutMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkOutDate.Month);
            string checkOutDay = checkOutDate.Day.ToString();

            SelectMonthAndDay(driver, checkInMonth, checkOutMonth, checkInDay, checkOutDay);
            FindSearchButtonAndClick(driver);
            VerifyThatEveryHotelContainsCity(driver, city);

            string actualDateStart = driver.FindElement(By.XPath("//button[@data-testid='date-display-field-start']")).Text;
            VerifyThatDateIsDispalyedInSearch(actualDateStart, checkInDay, checkInMonth);

            string actualDateEnd = driver.FindElement(By.XPath("//button[@data-testid='date-display-field-end']")).Text;
            VerifyThatDateIsDispalyedInSearch(actualDateEnd, checkOutDay, checkOutMonth);
        }

        private static void CloseModalWindowIdAppears(IWebDriver driver)
        {
            Console.WriteLine("Trying to find modal window and close it.");
            try
            {
                Thread.Sleep(5000);
                IWebElement modal = driver.FindElement(By.XPath("//div[@aria-modal='true']"));
                IWebElement btnCloseModal = driver.FindElement(By.XPath("//div[@aria-modal='true']//button"));
                btnCloseModal.Click();
            }
            catch (Exception)
            {
                Console.WriteLine("Modal window did not appear.");
            }
        }

        private static void ChangeLaguageToEnglishUk(IWebDriver driver)
        {
            Console.WriteLine("Change language to English - UK");
            IWebElement btnLangaugeSwitcher = driver.FindElement(By.XPath("//button[@data-testid = 'header-language-picker-trigger']"));
            btnLangaugeSwitcher.Click();
            IWebElement btnEnglishUk = driver.FindElement(By.XPath("(//span[text()='English (UK)'])[1]/ancestor::button"));
            btnEnglishUk.Click();
        }
        
        private static void SelectDestination(IWebDriver driver, string destination)
        {
            Console.WriteLine($"Selecting destination - {destination}");
            IWebElement txtDestination = driver.FindElement(By.XPath("//input[@name='ss']"));
            txtDestination.Clear();
            txtDestination.SendKeys(destination);
            try
            {
                Thread.Sleep(2000);
                IWebElement liDestination = driver.FindElement(By.XPath("//ul[contains(@aria-label, 'destination')]/li[1]"));
                liDestination.Click();
            }
            catch (Exception)
            {
                Console.WriteLine("Destinations dropdown did not appear");
            }
        }

        private static void FindAndClickCheckInCheckOut(IWebDriver driver)
        {
            Console.WriteLine("Click in Check-In/Check-Out button");
            IWebElement btnDate = null;
            try
            {
                btnDate = driver.FindElement(By.XPath("//div[@data-testid='searchbox-dates-container']//button[1]"));
            }
            catch (NoSuchElementException)
            {
                btnDate = driver.FindElement(By.XPath("(//span[contains(@class,'sb-date-field__icon')])[1]/*"));
            }
            catch (Exception)
            {
                Console.WriteLine("Гайки");
            }

            btnDate.Click();
            Thread.Sleep(2000);
        }

        private static void SelectMonthAndDay(IWebDriver driver, string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
        {
            Console.WriteLine("Selecting month and day");
            try
            {
                IWebElement leftWindow = driver.FindElement(By.XPath($"//div[contains(@class,'calendar')]//*[contains(text(), '{checkInMonth}')]"));
                IWebElement checkInDateToPick = leftWindow.FindElement(By.XPath($"./following-sibling::table//*[text()='{checkInDay}']"));
                checkInDateToPick.Click();

                IWebElement checkOutDateToPick;
                if (checkOutMonth != checkInMonth)
                {
                    IWebElement rightWindow = driver.FindElement(By.XPath($"//div[contains(@class,'calendar')]//*[contains(text(), '{checkOutMonth}')]"));
                    checkOutDateToPick = rightWindow.FindElement(By.XPath($"./following-sibling::table//*[text()='{checkOutDay}']"));
                }
                else
                {
                    checkOutDateToPick = leftWindow.FindElement(By.XPath($"./following-sibling::table//*[text()='{checkOutDay}']"));
                }

                checkOutDateToPick.Click();
                Console.WriteLine("\tTry block worked");
            }
            catch (Exception)
            {
                IWebElement leftWindow = driver.FindElement(By.XPath($"//h3[contains(text(), '{checkInMonth}')]/.."));
                IWebElement dateToPick = leftWindow.FindElement(By.XPath($".//*[text()='{checkInDay}']"));
                dateToPick.Click();

                IWebElement checkOutDateToPick;
                if (checkOutMonth != checkInMonth)
                {
                    IWebElement rightWindow = driver.FindElement(By.XPath($"//h3[contains(text(), '{checkOutMonth}')]/.."));
                    checkOutDateToPick = rightWindow.FindElement(By.XPath($".//*[text()='{checkOutDay}']"));
                }
                else
                {
                    checkOutDateToPick = leftWindow.FindElement(By.XPath($".//*[text()='{checkOutDay}']"));
                }

                checkOutDateToPick.Click();
                Console.WriteLine("\tCatch block worked");
            }
        }

        private static void FindSearchButtonAndClick(IWebDriver driver)
        {
            Console.WriteLine("Clicking search button");
            IWebElement btnSearch = driver.FindElement(By.XPath("//span[normalize-space()='Search']"));
            btnSearch.Click();
        }

        private static void VerifyThatEveryHotelContainsCity(IWebDriver driver, string city)
        {
            Console.WriteLine("Getting list of hotels");
            List<IWebElement> hotels = driver.FindElements(By.XPath("//div[@data-testid = 'property-card']")).ToList();
            int index = 0;
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"\tChecking hotel number {++index}");
                IWebElement hotelAddress = hotel.FindElement(By.XPath(".//span[@data-testid = 'address']"));
                string hotelAddressText = hotelAddress.Text;
                StringAssert.Contains(city, hotelAddressText, $"Actual hotel address does not contain {city}");
                Console.WriteLine($"\tHotel number {index} - verification passed.");
            }
        }

        private static void VerifyThatDateIsDispalyedInSearch(string actualDate, string expectedDay, string expectedMonth)
        {
            Console.WriteLine("Verify that date is dispayed in Search");
            string actualDayStart = actualDate.Split(' ')[1];
            string actualMonthStart = actualDate.Split(' ')[2];
            Assert.AreEqual(expectedDay, actualDayStart, "Check in day is not equal to expected");
            StringAssert.Contains(actualMonthStart, expectedMonth, "Check in month is not equal to expected");
            Console.WriteLine("Date is successfullt displayed.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
