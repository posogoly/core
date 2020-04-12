using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Posology.Core
{
    public class FrenchDrugDirectory : IDrugDirectory
    {

        private readonly string _path;

        public FrenchDrugDirectory(string path)
        {
            _path = path;
        }

        public string Search(string barCode)
        {
            //todo move files into blobs in azure
            var documents = Directory.GetFiles(_path).ToList();

            //todo stream list of medications
            var listOfContents = new List<List<string>>();

            var drugHeaderDetails = documents.Where(file => file.EndsWith("CIS.txt")).FirstOrDefault();
            var drugInfoWithBarcodes = documents.Where(file => file.EndsWith("CIS_CIP.txt")).FirstOrDefault();
            var drugCompositions = documents.Where(file => file.EndsWith("COMPO.txt")).FirstOrDefault();

            var drugs = GetDataFromHeaderFile(drugHeaderDetails);
            var drugPackages = AddDataFromPackageInfoFile(drugInfoWithBarcodes, drugs);
            AddDataFromDrugCompositionFile(drugInfoWithBarcodes, drugPackages);

            //todo search the given barcode
            var searchedDrug = drugPackages.Where(drug => drug.Barcode == barCode).FirstOrDefault();

            //todo return details
            return $"Found {searchedDrug.Description} in french drug directory";
        }

        private List<IDrug> GetDataFromHeaderFile(string filePath)
        {
            var items = new List<IDrug>();
            foreach (string row in File.ReadLines(filePath))
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

                items.Add(drug);
            }
            return items;
        }

        private List<IDrugPackaging> AddDataFromPackageInfoFile(string filePath, List<IDrug> drugs)
        {
            var items = new List<IDrugPackaging>();
            foreach (string row in File.ReadLines(filePath))
            {

                var drugDetails = row.Split('\t');
                var drugInfo = drugs.Where(drug => drug.InternalIdentifier == drugDetails[0]).FirstOrDefault();
                if (drugInfo != null)
                {
                    var package = new FrenchDrugPackaging(drugInfo)
                    {
                        Id = drugDetails[1],
                        Description = drugDetails[2],
                        Status = drugDetails[3],
                        CommercialisationStatus = drugDetails[4],
                        CommercialisationDate = drugDetails[5],
                        Barcode = drugDetails[6]
                    };

                    items.Add(package);
                }

                
            }
            return items;
        }

        private void AddDataFromDrugCompositionFile(string filePath, List<IDrugPackaging> packages)
        {
            foreach (string row in File.ReadLines(filePath))
            {

                var drugDetails = row.Split('\t');
                var packageInfo = packages.Where(package => package.InternalDrugIdentifier == drugDetails[0]).FirstOrDefault();

                var component = new FrenchDrugComponent(drugDetails[0])
                {
                    DrugShape = drugDetails[1],
                    ComponentId = drugDetails[2],
                    ComponentName = drugDetails[3],
                    ComponentAmount = drugDetails[4],
                    ComponentAmountUnit = drugDetails[5],
                    ComponentType = drugDetails[6],
                };

                packageInfo.Components.Add(component);

            }
        }
    }
}
