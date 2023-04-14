using RestSharp;

namespace APIHelper
{
    public class PetStoreClient
    {
        RestClient client = new RestClient("https://petstore.swagger.io/v2");

        public string CreatePet()
        {
            RestRequest request = new RestRequest("/pet", Method.Post);
            request.AddHeader("content-type", "application/json");
            string json = @"{
                ""id"": 8228,
                ""category"": {
                    ""id"": 0,
                    ""name"": ""Dorry""
                },
                ""name"": ""fish"",
                ""photoUrls"": [
                    ""string""],
                ""tags"": [
                    {
                        ""id"": 0,
                        ""name"": ""string""
                    }
                ],
                ""status"": ""available""
            }";
            request.AddBody(json);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Returned wrong status code");

            return response.Content;
        }

        public string GetPetById(int id)
        {
            RestRequest request = new RestRequest($"/pet/{id}", Method.Get);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception($"You did error in API \n\r {response.StatusCode}, {response.Content}");
            else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                throw new Exception($"Service unavailabe \n\r {response.StatusCode}, {response.Content}");
            return response.Content;
        }
    }
}