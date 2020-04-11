using Xunit;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
    {
        [Fact]
        public void ReadAllFilesInFrenchDataFolder()
        {
            const string path = "../../../Data/french-directory/fic_cis_cip/";
            var directory = new Core.FrenchDrugDirectory(path);
            var result = directory.Search("68269975");

            Assert.Equal("Found 3 in folder", result);
        }
    }
}
