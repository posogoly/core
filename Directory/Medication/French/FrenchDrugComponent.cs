using Posology.Core;

namespace Directory.Medication.French
{
    public class FrenchDrugComponent:IDrugComponent
    {
        public FrenchDrugComponent() { }
        public FrenchDrugComponent(string internalDrugIdentifier)
        {
            InternalDrugIdentifier = internalDrugIdentifier;
        }

        public string InternalDrugIdentifier { get; set; }
        public string DrugShape { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAmount { get; set; }
        public string ComponentAmountUnit { get; set; }
        public string ComponentType { get; set; }

        private bool Equals(IDrugComponent other)
        {
            return string.Equals(InternalDrugIdentifier, other.InternalDrugIdentifier) && 
                   string.Equals(DrugShape, other.DrugShape) && 
                   string.Equals(ComponentId, other.ComponentId) && 
                   string.Equals(ComponentName, other.ComponentName) && 
                   string.Equals(ComponentAmount, other.ComponentAmount) && 
                   string.Equals(ComponentAmountUnit, other.ComponentAmountUnit) && 
                   string.Equals(ComponentType, other.ComponentType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((FrenchDrugComponent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (InternalDrugIdentifier != null ? InternalDrugIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DrugShape != null ? DrugShape.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComponentId != null ? ComponentId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComponentName != null ? ComponentName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComponentAmount != null ? ComponentAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComponentAmountUnit != null ? ComponentAmountUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComponentType != null ? ComponentType.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}