using System;
using System.IO;
using System.Linq;
using Directory.Medication.French;
using Newtonsoft.Json;
using Posology.Core;
using Xunit;

namespace Directory.Tests
{
    public class UnitedStatesDirectoryShould : DirectoryShould
    {
        private readonly JsonSerializerSettings _settings;
        private const string Path = "../../../Data/french-directory/fic_cis_cip/";

        public UnitedStatesDirectoryShould()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
        }

        [Theory]
        [InlineData("3400931923077", "expected-result-3400931923077.json")]
        public async void ReadAllFilesInFrenchDataFolder(string barcode, string expectedResultFile)
        {

            var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var directory = new UnitedStatesDrugDirectory(rootDirectory, Path);

            var result = await directory.Search(barcode);

            Assert.Contains(barcode, result);

            
            var actual = JsonConvert.DeserializeObject<FrenchDrugPackaging>(result, _settings);

            var filePath = System.IO.Path.Combine(Path, expectedResultFile);
            var fileContent = File.ReadAllText(filePath);
            var expectedResult = JsonConvert.DeserializeObject<FrenchDrugPackaging>(fileContent, _settings);

            Assert.Equal(expectedResult, actual);

        }

    }
}
