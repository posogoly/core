using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Reflection;
using System.Net;
using System.Text;
using Directory;
using Directory.Medication.French;

namespace PosologyFunctions
{
    public static class PosologyReminder
    {
        [FunctionName("reminderservice")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "reminder/{countryCode}/")] HttpRequest req, string code,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            const string path = "./Data/french-directory/fic_cis_cip/";
            //todo use strategy pattern to determine which directory to use depending on country code

            var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootDirectory = Path.GetFullPath(Path.Combine(baseDirectory, ".."));
            var frenchDrugRepository = new FrenchDrugRepository(rootDirectory, path);
            var directory = new FrenchDrugDirectory(frenchDrugRepository, new FrenchLeafletRepository(new HttpClient()));

            //todo add to readme why the url is like that
            var result = await directory.Search(code);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };

        }
    }
}
