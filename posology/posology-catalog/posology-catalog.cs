using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Posology.Core;

namespace posologycatalog
{
    public static class posology_catalog
    {
        [FunctionName("posology_catalog")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "posology_catalog/barcode/{code}")]HttpRequestMessage req, string code, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            const string path = "./Data/french-directory/fic_cis_cip/";
            //todo use strategy pattern to determine which directory to use
            var directory = new FrenchDrugDirectory(path);
            //string barCode = "3400935887559";
            var result =  directory.Search(code);

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
