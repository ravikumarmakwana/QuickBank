using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace QuickBank.Functions
{
    public class AddQuarterlyInterest
    {
        private readonly HttpClient _client;
        public AddQuarterlyInterest()
        {
            _client = new HttpClient();
        }

        [Function("AddQuarterlyInterest")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request)
        {
            var result = await _client.PostAsync(
                $"{Environment.GetEnvironmentVariable("BaseURI")}/accounts/add-quarterly-interest",
                null
                );

            var response = request.CreateResponse();

            await response.WriteStringAsync(result.IsSuccessStatusCode ?
                "Quarterly Interest has been added successfully for all accounts" :
                "Failed to add Quarterly Interest for all accounts."
                );

            return response;
        }
    }
}
