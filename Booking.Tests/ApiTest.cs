using APIHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Tests
{
    public class ApiTest :BaseTest
    {
        [Test]
        public void ApiSampleTest()
        {
            PetStoreClient  client = new PetStoreClient();
            string result = client.CreatePet();
            StringAssert.Contains("Dorry", result);
        }

        [Test]
        public void OneMoreApiSampleTest()
        {
            PetStoreClient client = new PetStoreClient();
            string result = client.GetPetById(8228);
            StringAssert.Contains("Dorry", result);
        }
    }
}
