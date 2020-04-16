using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Directory.Medication.French;
using Newtonsoft.Json;

namespace Posology.Core
{
    public class UnitedStatesDrugDirectory : IDrugDirectory
    {
        private readonly string _rootDirectory;
        private readonly string _path;

        public UnitedStatesDrugDirectory(string rootDirectory, string path)
        {
            _rootDirectory = rootDirectory;
            //todo inject FileHelper in constructor
            _path = path;
        }

        public async Task<string> Search(string barCode)
        {
            //todo move files into blobs in azure?
            var drugHeaderDetails = "CIS.txt";
            var drugInfoWithBarcodes = "CIS_CIP.txt";
            var drugCompositions = "COMPO.txt";

            var drugPackage = await GetDataFromPackageInfoFile(drugInfoWithBarcodes, barCode);
            await AddDataFromDrugFile(drugHeaderDetails, drugPackage);
            await AddDataFromDrugCompositionFile(drugCompositions, drugPackage);

            //todo add found package to cache

            //todo handling special characters (in UI?)
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
                                           
            return JsonConvert.SerializeObject(drugPackage, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Ignore,
            });
            
        }

        private async Task<IDrugPackaging> GetDataFromPackageInfoFile(string filePath, string barCode)
        {
            var items = new List<IDrugPackaging>();
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);
            var row = fileContent.Where(line => line.Contains(barCode)).FirstOrDefault();
            if (row != null)
            {
                var drugDetails = row.Split('\t');

                var package = new FrenchDrugPackaging(drugDetails[0])
                {
                    Id = drugDetails[1],
                    Description = drugDetails[2],
                    Status = drugDetails[3],
                    CommercialisationStatus = drugDetails[4],
                    CommercialisationDate = drugDetails[5],
                    Barcode = drugDetails[6]
                };
                return package;
            }
            throw new KeyNotFoundException();
        }

        private async Task AddDataFromDrugFile(string filePath, IDrugPackaging drugPackage)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);
            var row = fileContent.Where(line => line.Contains(drugPackage.InternalDrugIdentifier)).FirstOrDefault();
            if (row != null)
            {
                var drugDetails = row.Split('\t');

                var drug = new FrenchDrug
                {
                    InternalIdentifier = drugDetails[0],
                    Denomination = drugDetails[1],
                    DrugType = drugDetails[2],
                    AutorisationStatus = drugDetails[4],
                    AdministrationType = drugDetails[3],
                    NoticeDocumentId = drugDetails[7]
                };
                drugPackage.Drug = drug;
            }
        }

        private async Task AddDataFromDrugCompositionFile(string filePath, IDrugPackaging package)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);

            foreach (string row in fileContent.Where(line => line.Contains(package.InternalDrugIdentifier)))
            {
                var drugDetails = row.Split('\t');

                var component = new FrenchDrugComponent(drugDetails[0])
                {
                    DrugShape = drugDetails[1],
                    ComponentId = drugDetails[2],
                    ComponentName = drugDetails[3],
                    ComponentAmount = drugDetails[4],
                    ComponentAmountUnit = drugDetails[5],
                    ComponentType = drugDetails[6],
                };
                //todo remove line 112 once refactoring complete
                package.AddComponent(component);
                package.Drug.AddComponent(component);
            }
        }

    }
}
