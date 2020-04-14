using System.IO;
using Posology.Core;
using Xunit;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
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

        private static void VerifyComponentData(FrenchDrugPackaging actual, FrenchDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Components.Count(), actual.Components.Count());
            var firstExpectedComponent = expectedResult.GetMainComponent();
            var firstActualComponent = actual.GetMainComponent();
            Assert.Equal(firstExpectedComponent.ComponentName, firstActualComponent.ComponentName);
            Assert.Equal(firstExpectedComponent.ComponentId, firstActualComponent.ComponentId);
            Assert.Equal(firstExpectedComponent.ComponentAmount, firstActualComponent.ComponentAmount);
            Assert.Equal(firstExpectedComponent.ComponentAmountUnit, firstActualComponent.ComponentAmountUnit);
            Assert.Equal(firstExpectedComponent.ComponentType, firstActualComponent.ComponentType);
            Assert.Equal(firstExpectedComponent.DrugShape, firstActualComponent.DrugShape);
        }

        private static void VerifyDrugData(FrenchDrugPackaging actual, FrenchDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Drug.Denomination, actual.Drug.Denomination);
            Assert.Equal(expectedResult.Drug.AdministrationType, actual.Drug.AdministrationType);
            Assert.Equal(expectedResult.Drug.AutorisationStatus, actual.Drug.AutorisationStatus);
            Assert.Equal(expectedResult.Drug.DrugType, actual.Drug.DrugType);
            Assert.Equal(expectedResult.Drug.UnkownNumber, actual.Drug.UnkownNumber);
            Assert.Equal(expectedResult.Drug.InternalIdentifier, actual.Drug.InternalIdentifier);
        }

        private static void VerifyPackageData(FrenchDrugPackaging actual, FrenchDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Barcode, actual.Barcode);
            Assert.Equal(expectedResult.Description, actual.Description);
            Assert.Equal(expectedResult.InternalDrugIdentifier, actual.InternalDrugIdentifier);
            Assert.Equal(expectedResult.Status, actual.Status);
            Assert.Equal(expectedResult.CommercialisationDate, actual.CommercialisationDate);
            Assert.Equal(expectedResult.CommercialisationStatus, actual.CommercialisationStatus);
            Assert.Equal(expectedResult.Id, actual.Id);
        }
    }
}
