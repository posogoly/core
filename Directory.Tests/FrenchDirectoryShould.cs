using System;
using System.IO;
using System.Net.Http;
using Directory.Medication.French;
using Newtonsoft.Json;
using Xunit;

namespace Directory.Tests
{
    public class FrenchDirectoryShould
    {
        private JsonSerializerSettings _settings;
        private FrenchLeafletRepository _frenchLeafletRepository;
        private FrenchDrugDirectory _directory;
        private readonly string _rootFolder = AppDomain.CurrentDomain.BaseDirectory;
        private const string Path = "../../../Data/french-directory/fic_cis_cip/";

        public FrenchDirectoryShould()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
            //todo mock client and stub the response
            var httpClient = new HttpClient();;
            _frenchLeafletRepository = new FrenchLeafletRepository(httpClient);
            
            _directory = new FrenchDrugDirectory(_rootFolder, Path,_frenchLeafletRepository);
        }
        
        [Theory]
        [InlineData("0328975", "expected-leaflet-0328975.json")]
        public async void Return_Drug_Leaflet_information(string leafletId, string expectedResultFile)
        {
            //todo mock client and stub the response
            var httpClient = new HttpClient();;
            var frenchLeafletRepository = new FrenchLeafletRepository(httpClient);
            var actualLeaflet = await frenchLeafletRepository.GetSideEffectFor(leafletId);

            var filePath = System.IO.Path.Combine(Path, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedLeaflet = JsonConvert.DeserializeObject<Leaflet>(fileContent, _settings);
            
            Assert.Equal(expectedLeaflet, actualLeaflet);

        }
        
        [Fact]
        public async void Return_DrugPackageBarcode()
        {
            const string barcode = "3400931923077";

            var result = await _directory.Search(barcode);

            Assert.Contains(barcode, result);

        }

        [Theory]
        [InlineData("3400935887559", "expected-result-3400935887559.json")]
        [InlineData("3400930065686", "expected-result-3400930065686.json")]
        [InlineData("3400931923077", "expected-result-3400931923077.json")]
        public async void Return_DrugPackageInformation_ByBarcode(string barcode, string expectedResultFile)
        {

            var result = await _directory.Search(barcode);
            var actual = JsonConvert.DeserializeObject<FrenchDrugPackaging>(result, _settings);

            var filePath = System.IO.Path.Combine(Path, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedResult = JsonConvert.DeserializeObject<FrenchDrugPackaging>(fileContent, _settings);

            Assert.Equal(expectedResult, actual);

        }

    }
}
