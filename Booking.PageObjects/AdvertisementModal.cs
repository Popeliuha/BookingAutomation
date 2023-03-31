using Framework;
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

        private Element modal => driver.FindElementsByXpath("//div[@aria-modal='true']").Where(x => x.IsDisplayed()).FirstOrDefault();
        private Element btnCloseModal => driver.FindElementByXpath("//div[@aria-modal='true']//button");

        private bool IsModalDisplayed()
        {
            return modal != null;
        }

        public void CloseModalIfExists()
        {
            if (IsModalDisplayed())
                btnCloseModal.Click();

            Helper.Wait(2);
        }
    }
}
