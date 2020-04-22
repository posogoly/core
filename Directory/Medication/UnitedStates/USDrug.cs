using System.Linq;
using Directory;

namespace Posology.Directory.Medication.UnitedStates
{
    public class USDrug : IDrug
    {
        private USDrugComponents _components;

        public USDrug()
        {
            _components = new USDrugComponents();
        }
        public string InternalIdentifier { get; set; }
        public string Denomination { get; set; }
        public string AutorisationStatus { get; set; }
        public string AdministrationType { get; set; }
        public string NoticeDocumentId { get; set; }
        public string DrugType { get; set; }
        public IDrugComponents Components { get => _components; set => _components = (USDrugComponents)value; }
        
        public void AddComponent(IDrugComponent component)
        {
            _components.Add((USDrugComponent)component);
        }

        public IDrugComponent GetMainComponent()
        {
            return _components?.FirstOrDefault();
        }
    }
}