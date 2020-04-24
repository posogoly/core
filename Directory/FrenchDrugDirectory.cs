using System.Threading.Tasks;
using Directory.Medication.French;

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

            var leaflet = await _leafletRepository.GetSideEffectFor(drugPackage.Drug.NoticeDocumentId);
            drugPackage.Leaflet = leaflet;

            //todo handling special characters (in UI?)

            return drugPackage.ToString();

        }



    }
}
