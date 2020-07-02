using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;

namespace FacebookEventsApi
{
    public class FacebookEventsController
    {
        private readonly HttpClient _client;

        public FacebookEventsController(HttpClient httpClient)
        {
            _client = httpClient;

            //_clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            //_clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        }

        [FunctionName("Health")]
        public async Task<IActionResult> RunHealthCheck(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for Health Check.");

            try
            {
                //var response = await _client.GetAsync($"https://api.foursquare.com/v2/venues/search?client_id={_clientId}&client_secret={_clientSecret}&v={_v}&near=new york&intent=browse&radius=10000&limit=10");
                //response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                return new OkObjectResult(HealthCheckResult.Unhealthy("An unhealthy result.", exception));
            }

            return new OkObjectResult(HealthCheckResult.Healthy("A healthy result."));
        }
    }
}
