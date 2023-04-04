using Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booking.PageObjects
{
    public class AdvertisementModal : BasePage
    {
        public AdvertisementModal(Driver driver) : base(driver)
        {

        }
        private string modalXPath = "//div[@aria-modal='true']";
        private Element modal => driver.FindElementsByXpath(modalXPath).Where(x => x.IsDisplayed()).FirstOrDefault();
        private Element btnCloseModal => driver.FindElementByXpath("//div[@aria-modal='true']//button");

        private bool IsModalDisplayed()
        {
            return modal != null;
        }

        public void CloseModalIfExists()
        {
            driver.WaitUntilElementExists(5, modalXPath);
            if (IsModalDisplayed())
                btnCloseModal.Click();

            driver.WaitUntilElementDisappearsFromDOM(5, modalXPath);
        }
    }
}
