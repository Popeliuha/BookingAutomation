using DatabaseHelper.DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Tests
{
    public class DataBaseTest : BaseTest
    {
        [Test]
        public void SampleTest()
        {
            int testId = 3;
            string userName = UserSql.GetUserNameById(testId);
            Assert.AreEqual("Valeriia", userName, "Got wrong username");
            Assert.AreNotEqual("Nataliia", userName, "");
        }


        [Test]
        public void OnMoreSampleTest()
        {
            int testId = 3;
            var user = UserSql.GetUserById(testId);
            Assert.AreEqual("Valeriia", user.UserName, "Got wrong username");
            Assert.AreEqual(74561, user.Mobile, "Got wrong mobile");
        }
    }
}
