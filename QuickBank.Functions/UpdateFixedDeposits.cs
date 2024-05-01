using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace QuickBank.Functions
{
    public class UpdateFixedDeposits
    {
        private readonly HttpClient _client;
        public UpdateFixedDeposits()
        {
            _client = new HttpClient();
        }

        [Function("UpdateFixedDeposits")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request)
        {
            var result = await _client.PostAsync(
                $"{Environment.GetEnvironmentVariable("BaseURI")}/fixed-deposits/update-fixed-deposits",
                null
                );

            var response = request.CreateResponse();

            await response.WriteStringAsync(result.IsSuccessStatusCode ?
                "Fixed Deposits has been updated successfully for all accounts" :
                "Failed to update Fixed Deposits for all accounts."
                );

            return response;
        }
    }
}
