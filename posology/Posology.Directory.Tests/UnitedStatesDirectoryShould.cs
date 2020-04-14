using System.IO;
using Posology.Core;
using Xunit;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Posology.Directory.Tests
{
    public class UnitedStatesDirectoryShould : DirectoryShould
    {
        const string PATH = "../../../Data/french-directory/fic_cis_cip/";

        [Theory]
        [InlineData("3400935887559", "expected-result-3400935887559.json")]
        [InlineData("3400930065686", "expected-result-3400930065686.json")]
        [InlineData("3400931923077", "expected-result-3400931923077.json")]
        public async void ReadAllFilesInFrenchDataFolder(string barcode, string expectedResultFile)
        {

            var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var directory = new UnitedStatesDrugDirectory(rootDirectory, PATH);

            var result = await directory.Search(barcode);

            Assert.Contains(barcode, result);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
            FrenchDrugPackaging actual = JsonConvert.DeserializeObject<FrenchDrugPackaging>(result, settings);

            var filePath = Path.Combine(PATH, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedResult = JsonConvert.DeserializeObject<FrenchDrugPackaging>(fileContent, settings);

            VerifyPackageData(actual, expectedResult);
            VerifyDrugData(actual.Drug, expectedResult.Drug);
            Assert.Equal(expectedResult.Components.Count(), actual.Components.Count());
            var firstExpectedComponent = expectedResult.GetMainComponent();
            var firstActualComponent = actual.GetMainComponent();
            VerifyComponentData(firstActualComponent, firstExpectedComponent);

        }

    }
}
