using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// Azure dependencies
using Microsoft.Rest.ClientRuntime;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Azure.Management.Billing;
using Microsoft.Azure.Management.Billing.Models;
using Microsoft.Azure.Management.Subscription;
using Microsoft.Azure.Management.Subscription.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Company.Function
{
    public static class CreateSubscription
    {
        [FunctionName("CreateSubscription")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
public static async Task<string> GetToken(string resource, string apiversion)
{
  string msiEndpoint = Environment.GetEnvironmentVariable("MSI_ENDPOINT");
  string endpoint = $"{msiEndpoint}/?resource={resource}&api-version={apiversion}";
  string msiSecret = Environment.GetEnvironmentVariable("MSI_SECRET");
  tokenClient.DefaultRequestHeaders.Add("Secret", msiSecret);
  JObject tokenServiceResponse = JsonConvert
      .DeserializeObject<JObject>(await tokenClient.GetStringAsync(endpoint));
  return tokenServiceResponse["access_token"].ToString();
}
