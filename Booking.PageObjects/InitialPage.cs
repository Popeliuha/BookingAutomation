using Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.PageObjects
{
    public class InitialPage : BasePage
    {
        public InitialPage(Driver driver) : base (driver)
        {

        }

        public AdvertisementModal AdvertisementModal => new AdvertisementModal(driver);
        public LanguageWindow LanguageWindow => new LanguageWindow(driver);

        private Element btnLangaugeSwitcher => driver.FindElementByXpath("//button[@data-testid = 'header-language-picker-trigger']");
        private Element txtDestination => driver.FindElementByXpath("//input[@name='ss']");
        private Element liDestination => driver.FindElementByXpath("//ul[contains(@aria-label, 'destination') ]/li[1]");
        private Element btnSearch => driver.FindElementByXpath("//span[normalize-space()='Search']");

        private Element GetBtnDate()
        {
            Element btnDate = null;

            try
            {
                btnDate = driver.FindElementByXpath("//div[@data-testid='searchbox-dates-container']//button[1]");
            }
            catch (NoSuchElementException)
            {
                btnDate = driver.FindElementByXpath("(//span[contains(@class,'sb-date-field__icon')])[1]/*");
            }
            return btnDate;
        }

        public void ClickLangaugeSwitcher()
        {
            btnLangaugeSwitcher.Click();
        }

        public void FillDestination(string destination)
        {
            txtDestination.Clear();
            txtDestination.SendText(destination);
        }

        public void CloseDestinationDropdownIfExists()
        {
            try
            {
                Helper.Wait(2);
                liDestination.Click();
            }
            catch (Exception)
            {
                Console.WriteLine("Destinations dropdown did not appear");
            }
        }

        public void ClickDate()
        {
            GetBtnDate().Click();
            Helper.Wait(2);
        }

        public void ClickSearch()
        {
            btnSearch.Click();
        }

        #region Month and Day Selection methods
        private void DefineFirstUIXpathesAndSelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
        {
            string leftWindowXPath = $"//div[contains(@class,'calendar')]//*[contains(text(), '{checkInMonth}')]";
            string checkInDateToPickXPath = $"./following-sibling::table//*[text()='{checkInDay}']";
            string rightWindowXPath = $"//div[contains(@class,'calendar')]//*[contains(text(), '{checkOutMonth}')]";
            string checkOutDateToPickXPath = $"./following-sibling::table//*[text()='{checkOutDay}']";
            SelectMonthAndDayAction(leftWindowXPath, checkInDateToPickXPath, rightWindowXPath, checkOutDateToPickXPath, checkInMonth, checkOutMonth);
        }

        private void DefineSecondUIXPathesAndSelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
        {
            string leftWindowXPath = $"//h3[contains(text(), '{checkInMonth}')]/..";
            string checkInDateToPickXPath = $".//*[text()='{checkInDay}']";
            string rightWindowXPath = $"//h3[contains(text(), '{checkOutMonth}')]/..";
            string checkOutDateToPickXPath = $".//*[text()='{checkOutDay}']";
            SelectMonthAndDayAction(leftWindowXPath, checkInDateToPickXPath, rightWindowXPath, checkOutDateToPickXPath, checkInMonth, checkOutMonth);
        }

        private void SelectMonthAndDayAction(string leftWindowXPath, string checkInDateToPickXPath, string rightWindowXPath, string checkOutDateToPickXPath,
            string checkInMonth, string checkOutMonth)
        {
            Element leftWindow = driver.FindElementByXpath(leftWindowXPath);
            Element checkInDateToPick = leftWindow.FindElementByXpath(checkInDateToPickXPath);
            checkInDateToPick.Click();

            Element checkOutDateToPick;
            if (checkOutMonth != checkInMonth)
            {
                Element rightWindow = driver.FindElementByXpath(rightWindowXPath);
                checkOutDateToPick = rightWindow.FindElementByXpath(checkOutDateToPickXPath);
            }
            else
            {
                checkOutDateToPick = leftWindow.FindElementByXpath(checkOutDateToPickXPath);
            }

            checkOutDateToPick.Click();
        }

        public void SelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
        {
            Helper.Wait(10);

            try
            {
                DefineFirstUIXpathesAndSelectMonthAndDay(checkInMonth, checkOutMonth, checkInDay, checkOutDay);
                Console.WriteLine("\tTry block worked");
            }
            catch (Exception)
            {
                DefineSecondUIXPathesAndSelectMonthAndDay(checkInMonth, checkOutMonth, checkInDay, checkOutDay);
                Console.WriteLine("\tCatch block worked");
            }
        }
        #endregion
    }
}
