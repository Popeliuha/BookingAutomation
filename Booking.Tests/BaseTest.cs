using Framework;
using NUnit.Framework;

namespace Booking.Tests
{
    public class BaseTest
    {
        protected Driver driver;

        [SetUp]
        public void Init()
        {
            driver = new DriverFactory().GetDriverByName("chrome");
            driver.GoToUrl("https://www.booking.com/");
            driver.MaximizeWindow();
        }


        [TearDown]
        public void TearDown()
        {
            driver.CloseDriver();
        }
    }
}
