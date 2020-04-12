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

            //var drugs = GetDataFromHeaderFile(drugHeaderDetails);

            var drugPackage = GetDataFromPackageInfoFile(drugInfoWithBarcodes, barCode);
            AddDataFromDrugFile(drugCompositions, drugPackage);
            AddDataFromDrugCompositionFile(drugCompositions, drugPackage);

            //todo add found package to cache

            //todo return details
            var mainComponent = drugPackage.Components.FirstOrDefault();
            return $"Found drug package with barcode {drugPackage.Barcode} with main component {mainComponent?.Denomination}";
        }

        private IDrugPackaging GetDataFromPackageInfoFile(string filePath, string barCode)
        {
            var items = new List<IDrugPackaging>();
            var row = File.ReadLines(filePath).Where(line => line.Contains(barCode)).FirstOrDefault();
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

        private void AddDataFromDrugFile(string filePath, IDrugPackaging drugPackage)
        {
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
                if (drugPackage.InternalDrugIdentifier == drug.InternalIdentifier)
                {
                    drugPackage.Drug = drug;
                    return;
                }
            }
        }

        private void AddDataFromDrugCompositionFile(string filePath, IDrugPackaging package)
        {
            foreach (string row in File.ReadLines(filePath).Where(line => line.Contains(package.InternalDrugIdentifier)))
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
