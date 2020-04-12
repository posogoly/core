using Xunit;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
    {
        [Fact]
        public async void ReadAllFilesInFrenchDataFolder()
        {
            const string path = "../../../Data/french-directory/fic_cis_cip/";
            var directory = new Core.FrenchDrugDirectory(path);
            var result = await directory.Search("3400935887559");
            var expectedResult = "Found drug package with barcode 3400935887559 with name SERESTA 10 mg, comprim� and main component OXAZ�PAM";

            Assert.Equal(expectedResult, result);

        }
    }
}
