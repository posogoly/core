using System.Linq;
using Posology.Directory;

namespace Directory.Medication.French
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
        public ILeaflet Leaflet { get; set; }
        public IDrug Drug { get => _drug; set => _drug = (FrenchDrug)value; }
        
        public void AddComponent(IDrugComponent component)
        {
            _components.Add((FrenchDrugComponent)component);
        }

        public IDrugComponent GetMainComponent()
        {
            return _components.FirstOrDefault();
        }

        private bool Equals(FrenchDrugPackaging other)
        {
            return Equals(_drug, other._drug) && Equals(_components, other._components) && 
                   string.Equals(InternalDrugIdentifier, other.InternalDrugIdentifier) && 
                   string.Equals(Description, other.Description) && 
                   string.Equals(Id, other.Id) && 
                   string.Equals(Status, other.Status) && 
                   string.Equals(CommercialisationStatus, other.CommercialisationStatus) && 
                   string.Equals(CommercialisationDate, other.CommercialisationDate) && 
                   string.Equals(Barcode, other.Barcode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FrenchDrugPackaging) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_drug != null ? _drug.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_components != null ? _components.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InternalDrugIdentifier != null ? InternalDrugIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Status != null ? Status.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CommercialisationStatus != null ? CommercialisationStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CommercialisationDate != null ? CommercialisationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Barcode != null ? Barcode.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}