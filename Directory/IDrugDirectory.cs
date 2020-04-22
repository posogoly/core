using System.Threading.Tasks;

namespace Directory
{
    public interface IDrugDirectory
    {
        Task<string> Search(string barCode);
    }
}