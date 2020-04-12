using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Posology.Core
{
    public class FrenchDrugDirectory : IDrugDirectory
    {

        private readonly string _path;

        public FrenchDrugDirectory(string path)
        {
            _path = path;
        }

        public async Task<string> Search(string barCode)
        {
            //todo move files into blobs in azure

            var docs = await FileHelper.AsyncGetFiles(_path);
            //todo stream list of medications
            var listOfContents = new List<List<string>>();

            var drugHeaderDetails = docs.Where(file => file.EndsWith("CIS.txt")).FirstOrDefault();
            var drugInfoWithBarcodes = docs.Where(file => file.EndsWith("CIS_CIP.txt")).FirstOrDefault();
            var drugCompositions = docs.Where(file => file.EndsWith("COMPO.txt")).FirstOrDefault();

            //var drugs = GetDataFromHeaderFile(drugHeaderDetails);

            var drugPackage = await GetDataFromPackageInfoFile(drugInfoWithBarcodes, barCode);
            await AddDataFromDrugFile(drugCompositions, drugPackage);
            await AddDataFromDrugCompositionFile(drugCompositions, drugPackage);

            //todo add found package to cache

            //todo return details handling special characters
            var mainComponent = drugPackage.Components.FirstOrDefault();
            return $"Found drug package with barcode {drugPackage.Barcode} with main component {mainComponent?.ComponentName}";
        }

        private async Task<IDrugPackaging> GetDataFromPackageInfoFile(string filePath, string barCode)
        {
            var items = new List<IDrugPackaging>();
            var fileContent = await FileHelper.ReadAllLinesAsync(filePath, Encoding.UTF8);
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
            var fileContent = await FileHelper.ReadAllLinesAsync(filePath, Encoding.UTF8);
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
                    UnkownNumber = drugDetails[7]
                };
                drugPackage.Drug = drug;
            }
        }

        private async Task AddDataFromDrugCompositionFile(string filePath, IDrugPackaging package)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(filePath, Encoding.UTF8);

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

                package.Components.Add(component);
            }
        }

    }
}
