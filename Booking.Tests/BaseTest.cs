using Framework;
using NLog;
using NUnit.Framework;

namespace Booking.Tests
{
    public class BaseTest
    {
        protected Driver driver;
        protected static Logger logger = LogManager.GetCurrentClassLogger();

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

        public void WriteLog(string message)
        {
            TestContext.Out.WriteLine("\n" + message);
            logger.Debug(message);
        }
    }
}
