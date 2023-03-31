using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.PageObjects
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(Driver driver):base(driver)
        {

        }

        private List<Element> hotelsList => driver.FindElementsByXpath("//div[@data-testid = 'property-card']");
        private Element actualDateStart => driver.FindElementByXpath("//button[@data-testid='date-display-field-start']");
        private Element actualDateEnd => driver.FindElementByXpath("//button[@data-testid='date-display-field-end']");

        public string GetActualDateStartText() => actualDateStart.GetText();
        public string GetActualDateEndText() => actualDateEnd.GetText();

        public List<string> GetHotelsAdresses()
        {
            List<string> result = new List<string>();

            foreach (var hotel in hotelsList)
            {
                Element hotelAddress = hotel.FindElementByXpath(".//span[@data-testid = 'address']");
                string hotelAddressText = hotelAddress.GetText();
                result.Add(hotelAddressText);
            }

            return result;
        }
    }
}