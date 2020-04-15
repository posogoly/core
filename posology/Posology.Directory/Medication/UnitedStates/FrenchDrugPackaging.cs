using System.Linq;
using Posology.Core;

namespace Posology.Directory.Medication.UnitedStates
{
    public class USDrugPackaging: IDrugPackaging
    {
        private USDrug _drug;
        private USDrugComponents _components;

        public USDrugPackaging() {
            _components = new USDrugComponents();
        }
        public USDrugPackaging(string internalIdentifier)
        {
            InternalDrugIdentifier = internalIdentifier;
            _components =  new USDrugComponents();
        }

        public string InternalDrugIdentifier { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string CommercialisationStatus { get; set; }
        public string CommercialisationDate { get; set; }
        public string Barcode { get; set; }
        public IDrugComponents Components { get => _components; set => _components = (USDrugComponents)value; }
        public IDrug Drug { get => _drug; set => _drug = (USDrug)value; }
        
        public void AddComponent(IDrugComponent component)
        {
            _components.Add((USDrugComponent)component);
        }

        public IDrugComponent GetMainComponent()
        {
            return _components.FirstOrDefault();
        }
    }
}