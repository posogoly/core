using System.Threading.Tasks;

namespace Directory
{
    public class FrenchDrugDirectory : IDrugDirectory
    {
        
        private readonly IDrugRepository _drugRepository;
        private readonly FrenchLeafletRepository _leafletRepository;

        public FrenchDrugDirectory(IDrugRepository drugRepository ,FrenchLeafletRepository leafletRepository)
        {
            _drugRepository = drugRepository;
            _leafletRepository = leafletRepository;
        }

        public async Task<string> Search(string barCode)
        {
            //todo move files into blobs in azure?

            var drugPackage = await _drugRepository.GetBy(barCode);

            drugPackage.Leaflet = await _leafletRepository.GetSideEffectFor(drugPackage.GetLeafletId());

            //todo handling special characters (in UI?)

            return drugPackage.ToString();

        }



    }
}
