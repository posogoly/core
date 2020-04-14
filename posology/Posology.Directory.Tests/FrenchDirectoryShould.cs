using System.IO;
using Posology.Core;
using Xunit;
using Newtonsoft.Json;
using System;

namespace Posology.Directory.Tests
{
    public class FrenchDirectoryShould : DirectoryShould
    {
        const string PATH = "../../../Data/french-directory/fic_cis_cip/";

        [Theory]
        [InlineData("3400935887559", "expected-result-3400935887559.json")]
        [InlineData("3400930065686", "expected-result-3400930065686.json")]
        [InlineData("3400931923077", "expected-result-3400931923077.json")]
        public async void ReadAllFilesInFrenchDataFolder(string barcode, string expectedResultFile)
        {

            var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var directory = new FrenchDrugDirectory(rootDirectory, PATH);

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
            VerifyDrugData(actual, expectedResult);
            VerifyComponentData(actual, expectedResult);

        }

    }
}
