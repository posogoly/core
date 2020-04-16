using System.Threading.Tasks;

namespace Posology.Core
{
    public interface IDrugDirectory
    {
        Task<string> Search(string barCode);
    }
}