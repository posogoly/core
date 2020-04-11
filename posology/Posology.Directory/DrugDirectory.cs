using System.IO;
using System.Collections.Generic;
using CsvHelper;
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
            string[] documents = Directory.GetFiles(_path);

            //todo stream list of medications
            var listOfContents = new List<List<string>>();
            var drugs = new List<IDrug>();
            foreach (string file in documents)
            {
                if (file.EndsWith("CIS.txt"))
                {
                    drugs = GetDataFromFile(file);
                }
            }

            //todo search the given barcode
            var searchedDrug = drugs.Where(drug => drug.InternalIdentifier == barCode).FirstOrDefault();
            
            //todo return details
            return $"Found {searchedDrug.Denomination} in french drug directory";
        }

        private List<IDrug> GetDataFromFile(string filePath)
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

        private IEnumerable<T> ParseDataFromFile<T>(string filePath)
        {
            TextReader reader = File.OpenText(filePath);
            CsvReader csvFile = new CsvReader((IParser)reader);
            csvFile.Configuration.HasHeaderRecord = true;
            csvFile.Read();
            var records = csvFile.GetRecords<T>();
            return records;
        }
    }
}
