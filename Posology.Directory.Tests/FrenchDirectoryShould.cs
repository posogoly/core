using System.IO;
using Posology.Core;
using Xunit;
using Newtonsoft.Json;
using System;
using System.Linq;
using Posology.Directory.Medication.French;

namespace Posology.Directory.Tests
{
    public class FrenchDirectoryShould : DirectoryShould
    {
        private const string Path = "../../../Data/french-directory/fic_cis_cip/";

        [Fact]
        public async void Return_DrugPackageBarcode()
        {
            var barcode = "3400931923077";
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

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };

            //todo refactor in narrative of scenario ATDD
            var actual = JsonConvert.DeserializeObject<FrenchDrugPackaging>(result, settings);

            var filePath = System.IO.Path.Combine(Path, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedResult = JsonConvert.DeserializeObject<FrenchDrugPackaging>(fileContent, settings);

            //todo move to equality function in data structure
            VerifyPackage(actual, expectedResult);
            VerifyDrugData(actual.Drug, expectedResult.Drug);
            //todo move component to drug data structure
            Assert.Equal(expectedResult.Components.Count(), actual.Components.Count());
            var firstExpectedComponent = expectedResult.GetMainComponent();
            var firstActualComponent = actual.GetMainComponent();
            VerifyComponentData(firstActualComponent, firstExpectedComponent);

        }

    }
}
