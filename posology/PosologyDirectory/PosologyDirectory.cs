using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Posology.Core;
using System.Net.Http;
using System.Net;
using System.Text;

namespace PosologyDirectory
{
    public static class PosologyDirectory
    {
        [FunctionName("PosologyDirectory")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "directory/barcode/{countryCode}/{code}")] HttpRequest req, string code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            const string path = "./Data/french-directory/fic_cis_cip/";
            //todo use strategy pattern to determine which directory to use depending on country code
            var directory = new FrenchDrugDirectory(path);

            //string barCode = "3400935887559";
            var result = await directory.Search(code);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };

        }
    }
}
