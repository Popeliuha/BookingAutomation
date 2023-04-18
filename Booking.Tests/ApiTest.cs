using APIHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ApiTest :BaseTest
    {
        [Test]
        public void ApiSampleTest()
        {
            PetStoreClient  client = new PetStoreClient();
            string result = client.CreatePet();
            StringAssert.Contains("Dorry", result);
            Thread.Sleep(1000);
            WriteLog("Test has passed");

        }

        [Test]
        public void OneMoreApiSampleTest()
        {
            PetStoreClient client = new PetStoreClient();
            string result = client.GetPetById(8228);
            StringAssert.Contains("Dorry", result);
            Thread.Sleep(1000);
            WriteLog("Test has passed");
        }
    }
}
