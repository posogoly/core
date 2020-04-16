using System.Linq;
using Posology.Core;

namespace Posology.Directory.Medication.French
{
    public class FrenchDrugPackaging: IDrugPackaging
    {
        private FrenchDrug _drug;
        private FrenchDrugComponents _components;

        public FrenchDrugPackaging() {
            _components = new FrenchDrugComponents();
        }
        public FrenchDrugPackaging(string internalIdentifier)
        {
            InternalDrugIdentifier = internalIdentifier;
            _components =  new FrenchDrugComponents();
        }

        public string InternalDrugIdentifier { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string CommercialisationStatus { get; set; }
        public string CommercialisationDate { get; set; }
        public string Barcode { get; set; }
        public IDrugComponents Components { get => _components; set => _components = (FrenchDrugComponents)value; }
        public IDrug Drug { get => _drug; set => _drug = (FrenchDrug)value; }
        
        public void AddComponent(IDrugComponent component)
        {
            _components.Add((FrenchDrugComponent)component);
        }

        public IDrugComponent GetMainComponent()
        {
            return _components.FirstOrDefault();
        }
    }
}