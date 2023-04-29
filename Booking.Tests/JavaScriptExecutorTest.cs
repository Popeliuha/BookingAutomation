using NUnit.Framework;

namespace Booking.Tests
{
    [TestFixture]
    public class JavaScriptExecutorTest : BaseTest
    {
        [Test]
        public void JsTest()
        {
            driver.ExecuteJsCommand("window.scrollTo(0, document.body.scrollHeight);");
            logger.Info("This is info");
        }
    }
}
