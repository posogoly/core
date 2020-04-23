using System;
using System.IO;
using Directory.Medication.French;
using Newtonsoft.Json;
using Xunit;

namespace Directory.Tests
{
    public class FrenchDirectoryShould
    {
        private JsonSerializerSettings _settings;
        private const string Path = "../../../Data/french-directory/fic_cis_cip/";

        public FrenchDirectoryShould()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
        }
        
        [Fact]
        public async void Return_Drug_Leaflet_information()
        {
            const string leafletId = "0328975";
            var rootFolder = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new FrenchDrugDirectory(rootFolder, Path);

            var result = await directory.GetSideEffectFor(leafletId);

            Assert.Contains("EFFETS INDESIRABLES EVENTUELS", result.RawContent);

        }
        
        [Fact]
        public async void Return_DrugPackageBarcode()
        {
            const string barcode = "3400931923077";
            var rootFolder = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new FrenchDrugDirectory(rootFolder, Path);

            var result = await directory.Search(barcode);

            Assert.Contains(barcode, result);

        }

        [Theory]
        [InlineData("3400935887559", "expected-result-3400935887559.json")]
        [InlineData("3400930065686", "expected-result-3400930065686.json")]
        [InlineData("3400931923077", "expected-result-3400931923077.json")]
        public async void Return_DrugPackageInformation_ByBarcode(string barcode, string expectedResultFile)
        {

            var rootFolder = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new FrenchDrugDirectory(rootFolder, Path);

            var result = await directory.Search(barcode);
            var actual = JsonConvert.DeserializeObject<FrenchDrugPackaging>(result, _settings);

            var filePath = System.IO.Path.Combine(Path, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedResult = JsonConvert.DeserializeObject<FrenchDrugPackaging>(fileContent, _settings);

            Assert.Equal(expectedResult, actual);

        }

    }
}
