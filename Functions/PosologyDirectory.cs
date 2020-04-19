using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Posology.Core;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO;
using System.Reflection;

namespace PosologyDirectory
{
    public static class PosologyDirectory
    {
        [FunctionName("Directory")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "directory/{countryCode}/barcode/{code}")] HttpRequest req, string code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            const string path = "./Data/french-directory/fic_cis_cip/";
            //todo use strategy pattern to determine which directory to use depending on country code

            var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootDirectory = Path.GetFullPath(Path.Combine(baseDirectory, ".."));

            var directory = new FrenchDrugDirectory(rootDirectory, path);

            //todo remive this string barCode = "3400935887559";
            //todo add to readme why the url is like that
            var result = await directory.Search(code);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };

        }
    }
}
