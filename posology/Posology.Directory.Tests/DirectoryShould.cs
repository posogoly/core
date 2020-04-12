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

            Assert.Equal("Found drug package with barcode 3400935887559 with main component OXAZ�PAM", result);
        }
    }
}
