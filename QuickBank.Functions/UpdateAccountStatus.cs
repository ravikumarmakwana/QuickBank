using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace QuickBank.Functions
{
    public class UpdateAccountStatus
    {
        private readonly HttpClient _client;
        public UpdateAccountStatus()
        {
            _client = new HttpClient();
        }

        [Function("UpdateAccountStatus")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData request)
        {
            var result = await _client.PostAsync(
                $"{Environment.GetEnvironmentVariable("BaseURI")}/transactions/update-account-status",
                null
                );

            var response = request.CreateResponse();

            await response.WriteStringAsync(result.IsSuccessStatusCode ?
                "Update Account Status has been updated successfully." :
                "Failed to update account status."
                );

            return response;
        }
    }
}
