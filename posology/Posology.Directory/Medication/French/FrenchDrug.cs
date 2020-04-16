using System.Linq;
using Posology.Core;

namespace Posology.Directory.Medication.French
{
    public class FrenchDrug : IDrug
    {
        private FrenchDrugComponents _components;

        public FrenchDrug()
        {
            _components = new FrenchDrugComponents();
        }
        public string InternalIdentifier { get; set; }
        public string Denomination { get; set; }
        public string AutorisationHolder { get; set; }
        public string HeldSince { get; set; }
        public string AutorisedSince { get; set; }
        public string AutorisationStatus { get; set; }
        public string PrescriptionList { get; set; }
        public string AdministrationType { get; set; }
        public string PrescriptionDetails { get; set; }
        public string PackagingDetails { get; set; }
        public string DrugType { get; set; }
        public IDrugComponents Components { get => _components; set => _components = (FrenchDrugComponents)value; }
        public string NoticeDocumentId { get; set; }
        
        public void AddComponent(IDrugComponent component)
        {
            _components.Add((FrenchDrugComponent)component);
        }

        public IDrugComponent GetMainComponent()
        {
            return _components?.FirstOrDefault();
        }
    }
}