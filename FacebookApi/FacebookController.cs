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
using FacebookApi.Client;

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
                var response = await _client.GetAsync($"https://graph.facebook.com/search?type=place&center=51.1375982,-3.0087667&distance=1&access_token=566945730659600|J0GkU5iDaQGU8pMhJ9gSmW5pfrg");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                return new OkObjectResult(HealthCheckResult.Unhealthy("An unhealthy result.", exception));
            }

            return new OkObjectResult(HealthCheckResult.Healthy("A healthy result."));
        }

        [FunctionName("Places")]
        public async Task<IActionResult> RunGetPlaces(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            PlacesResponse result = new PlacesResponse();

            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string name = req.Query["name"];

                var latQueryParameter = req.Query["lat"].ToString().Trim('f', 'F');
                if (string.IsNullOrEmpty(latQueryParameter))
                    return new BadRequestObjectResult("Please pass a lat parameter");

                var lonQueryParameter = req.Query["lon"].ToString().Trim('f', 'F');
                if (string.IsNullOrEmpty(lonQueryParameter))
                    return new BadRequestObjectResult("Please pass a lon parameter");

                string radius = "10000";
                var radiusQueryParameter = req.Query["radius"].ToString();
                if (!string.IsNullOrEmpty(radiusQueryParameter))
                    radius = radiusQueryParameter;

                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var response = await _client.GetAsync($"https://graph.facebook.com/search?type=place&center={latQueryParameter},{lonQueryParameter}&distance={radius}&fields=name,location,about,category_list,description,hours,overall_star_rating,phone,restaurant_services,restaurant_specialties,single_line_address&access_token=566945730659600|J0GkU5iDaQGU8pMhJ9gSmW5pfrg");
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                result = await response.Content.ReadAsAsync<PlacesResponse>();
            }
            catch (Exception exception)
            {
                // Error
            }

            return new OkObjectResult(result);
        }
    }
}
