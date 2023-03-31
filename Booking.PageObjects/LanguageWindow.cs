using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.PageObjects
{
    public class LanguageWindow : BasePage
    {
        public LanguageWindow(Driver driver) :base(driver)
        {

        }

        private Element btnLanguage(string language) => driver.FindElementByXpath($"(//span[text()='{language}'])[1]/ancestor::button");

        public void SelectLanguage(string language)
        {
            btnLanguage(language).Click();
        }
    }
}
