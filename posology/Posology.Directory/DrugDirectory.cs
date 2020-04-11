using System.IO;
using System.Collections.Generic;

namespace Posology.Core
{
    public class DrugDirectory
    {
        public string Search(string barCode)
        {
            //string filepath = null;
            //todo move files into blobs in azure
            string[] documents = System.IO.Directory.GetFiles("../../../Data/french-directory/fic_cis_cip/");
            //todo stream list of medications



            //todo search the given barcode
            //todo return details
            return $"Found {documents.Length} in folder";
        }

        private IEnumerable<T> GetFields<T>(string filepath)
        {
            var medications = new List<string>();
            foreach (string row in File.ReadLines(filepath))
            {
                foreach (string field in row.Split(',')) {
                     List<string> items = new List<string>();
                    medications.Add(field);
                }
            }
            return (IEnumerable<T>)medications;
        }
    }
}
