using Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Tests
{
    public class JavaScriptExecutorTest : BaseTest
    {
        [Test]
        public void JsTest()
        {
            driver.ExecuteJsCommand("window.scrollTo(0, document.body.scrollHeight);");
            Helper.Wait(5);
            logger.Info("This is info");
            logger.Warn("This is info");
            logger.Error("This is info");
            logger.Debug("This is info");
        }
    }
}
