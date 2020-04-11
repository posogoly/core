using Xunit;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
    {
        [Fact]
        public void ReadAllFilesInFrenchDataFolder()
        {
            var directory = new Core.DrugDirectory();
            var result = directory.Search("3400935887559");

            Assert.Equal("Found 3 in folder", result);
        }
    }
}
