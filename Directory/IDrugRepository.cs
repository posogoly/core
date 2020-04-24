using System.Threading.Tasks;

namespace Directory
{
    public interface IDrugRepository
    {
        Task<IDrugPackaging> GetBy(string barCode);
    }
}