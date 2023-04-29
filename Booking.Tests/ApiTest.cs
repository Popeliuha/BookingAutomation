using APIHelper;
using NUnit.Framework;

namespace Booking.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ApiTest : BaseTest
    {
        [Test]
        public void ApiSampleTest()
        {
            PetStoreClient client = new PetStoreClient();
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
