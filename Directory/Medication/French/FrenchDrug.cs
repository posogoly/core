using System.Linq;
using Posology.Core;
using Posology.Directory;

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

        private bool Equals(FrenchDrug other)
        {
            return Equals(_components, other._components) && 
                   string.Equals(InternalIdentifier, other.InternalIdentifier) && 
                   string.Equals(Denomination, other.Denomination) && 
                   string.Equals(AutorisationHolder, other.AutorisationHolder) && 
                   string.Equals(HeldSince, other.HeldSince) && 
                   string.Equals(AutorisedSince, other.AutorisedSince) && 
                   string.Equals(AutorisationStatus, other.AutorisationStatus) && 
                   string.Equals(PrescriptionList, other.PrescriptionList) && 
                   string.Equals(AdministrationType, other.AdministrationType) && 
                   string.Equals(PrescriptionDetails, other.PrescriptionDetails) && 
                   string.Equals(PackagingDetails, other.PackagingDetails) && 
                   string.Equals(DrugType, other.DrugType) && 
                   string.Equals(NoticeDocumentId, other.NoticeDocumentId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FrenchDrug) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_components != null ? _components.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InternalIdentifier != null ? InternalIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Denomination != null ? Denomination.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AutorisationHolder != null ? AutorisationHolder.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HeldSince != null ? HeldSince.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AutorisedSince != null ? AutorisedSince.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AutorisationStatus != null ? AutorisationStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PrescriptionList != null ? PrescriptionList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AdministrationType != null ? AdministrationType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PrescriptionDetails != null ? PrescriptionDetails.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PackagingDetails != null ? PackagingDetails.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DrugType != null ? DrugType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NoticeDocumentId != null ? NoticeDocumentId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}