using Framework;
using System;

namespace Booking.PageObjects
{
    public class BasePage
    {
        protected static Driver driver;

        public BasePage(Driver webDriver)
        {
            driver = webDriver;
        }
    }
}
