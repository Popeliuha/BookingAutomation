using ER = AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Framework;
using NLog;
using NUnit.Framework;
using System;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace Booking.Tests
{
    public class BaseTest
    {
        protected Driver driver;
        protected ER.ExtentReports extentReports;
        protected ER.ExtentTest extentTest;
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        struct ContextOfTest
        {
            public Status status;
            public string stackTrace;
            public string errorMessage;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var testClassName = TestContext.CurrentContext.Test.ClassName;
            var date = DateTime.Now.ToString(" dd-MM-yyyy_(HH_mm_ss)");
            var outputDir = $"{Pathes.REPORTS_PATH}\\{testClassName}{date}\\";
            var param = "Automation_Report.html";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter($"{outputDir}{param}");
            extentReports = new ER.ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void Init()
        {
            extentTest = extentReports.CreateTest(TestContext.CurrentContext.Test.MethodName);
            driver = new DriverFactory().GetDriverByName(BrowserEnum.Chrome);
            driver.GoToUrl("https://www.booking.com/");
            driver.MaximizeWindow();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                ContextOfTest test;
                test.stackTrace = TestContext.CurrentContext.Result.StackTrace;
                test.errorMessage = TestContext.CurrentContext.Result.Message;
                test.status = GetStatus(TestContext.CurrentContext.Result.Outcome.Status);

                if (test.status == Status.Fail)
                {
                    AddTestHTML(test, TakeScreenshot());
                }
                else
                {
                    AddTestHTML(test);
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                driver?.CloseDriver();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extentReports.Flush();
        }

        public string TakeScreenshot()
        {
            string dir = Pathes.SCREENSHOTS_PATH;
            string screenshotFullName = driver.TakeScreenshot(dir);
            return screenshotFullName;
        }

        public void WriteLog(string message)
        {
            TestContext.Out.WriteLine("\n" + message);
            logger.Debug(message);
            extentTest.Log(Status.Info, message);
        }

        private Status GetStatus(TestStatus testStatus)
        {
            switch (testStatus)
            {
                case TestStatus.Failed:
                    return Status.Fail;
                case TestStatus.Skipped:
                    return Status.Skip;
                default:
                    return Status.Pass;
            }
        }

        private void AddTestHTML(ContextOfTest test, string screenshotPath = null)
        {
            string stackTrace = string.IsNullOrEmpty(test.stackTrace) ? "\n<br>" : $"\n<br>{test.stackTrace}\n<br>";
            string errorMessage = string.IsNullOrEmpty(test.errorMessage) ? "\n<br>" : $"\n<br>{test.errorMessage}\n<br>";
            extentTest.Log(test.status, $"Test ended with {test.status}, {stackTrace}{errorMessage}");
            if (screenshotPath != null)
            {
                AddScreenshotToHTML(test.status, screenshotPath);
            }
        }

        private void  AddScreenshotToHTML(Status status, string screenshotPath)
        {
            extentTest.Log(status, $"Screenshot attached: {extentTest.AddScreenCaptureFromPath(screenshotPath)}");
        }
        
    }
}
