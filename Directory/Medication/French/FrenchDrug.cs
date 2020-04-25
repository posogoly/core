using System.Linq;

namespace Directory.Medication.French
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
        public string AutorisationStatus { get; set; }
        public string AdministrationType { get; set; }
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

        private bool Equals(FrenchDrug other)
        {
            return Equals(_components, other._components) && 
                   string.Equals(InternalIdentifier, other.InternalIdentifier) && 
                   string.Equals(Denomination, other.Denomination) && 
                   string.Equals(AutorisationStatus, other.AutorisationStatus) && 
                   string.Equals(AdministrationType, other.AdministrationType) &&  
                   string.Equals(DrugType, other.DrugType) && 
                   string.Equals(NoticeDocumentId, other.NoticeDocumentId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FrenchDrug) obj);
        }

    }
}