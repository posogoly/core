using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Posology.Core;

namespace PosologyDirectory
{
    public static class PosologyDirectory
    {
        [FunctionName("PosologyDirectory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "posologydirectory/barcode/{code}")] HttpRequest req, string code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            const string path = "./Data/french-directory/fic_cis_cip/";
            //todo use strategy pattern to determine which directory to use
            var directory = new FrenchDrugDirectory(path);

            //string barCode = "3400935887559";
            var result = await directory.Search(code);

            return new OkObjectResult(result);

        }
    }
}
