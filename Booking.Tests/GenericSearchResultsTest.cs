using Booking.PageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Booking.Tests
{
    public class GenericSearchResultsTest : BaseTest
    {
        [Test]
        public void VerifyGenericSearchResults()
        {
            string city = "New York";
            InitialPage initialPage = new InitialPage(driver);
            Console.WriteLine("Close modal if exists");
            initialPage.AdvertisementModal.CloseModalIfExists();

            Console.WriteLine("Change language to English - UK");
            initialPage.ClickLangaugeSwitcher();
            initialPage.LanguageWindow.SelectLanguage("English (UK)");

            Console.WriteLine($"Selecting destination - {city}");
            initialPage.FillDestination(city);
            initialPage.CloseDestinationDropdownIfExists();

            Console.WriteLine("Click in Check-In/Check-Out button");
            initialPage.ClickDate();

            DateTime checkInDate = DateTime.Now.AddDays(1);
            string checkInMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkInDate.Month);
            string checkInDay = checkInDate.Day.ToString();

            DateTime checkOutDate = checkInDate.AddDays(7);
            string checkOutMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkOutDate.Month);
            string checkOutDay = checkOutDate.Day.ToString();

            Console.WriteLine("Selecting month and day");
            initialPage.SelectMonthAndDay(checkInMonth, checkOutMonth, checkInDay, checkOutDay);

            Console.WriteLine("Clicking search button");
            initialPage.ClickSearch();

            Console.WriteLine("Getting list of hotels");
            SearchResultsPage searchResultsPage = new SearchResultsPage(driver);
            List<string> hotelsAddresses = searchResultsPage.GetHotelsAdresses();

            foreach (var hotelAddressText in hotelsAddresses)
                StringAssert.Contains(city, hotelAddressText, $"Actual hotel address does not contain {city}");

            string actualDateStart = searchResultsPage.GetActualDateStartText();
            string actualDateEnd = searchResultsPage.GetActualDateEndText();

            VerifyThatDateIsDispalyedInSearch(actualDateStart, checkInDay, checkInMonth);
            VerifyThatDateIsDispalyedInSearch(actualDateEnd, checkOutDay, checkOutMonth);
        }

        private void VerifyThatDateIsDispalyedInSearch(string actualDate, string expectedDay, string expectedMonth)
        {
            Console.WriteLine($"Verify that date {actualDate} is dispayed in Search");
            string actualDay = actualDate.Split(' ')[1];
            string actualMonth = actualDate.Split(' ')[2];

            Assert.AreEqual(expectedDay, actualDay, "Check in day is not equal to expected");
            StringAssert.Contains(actualMonth, expectedMonth, "Check in month is not equal to expected");
            Console.WriteLine("Date is successfully displayed.");
        }
    }
}
