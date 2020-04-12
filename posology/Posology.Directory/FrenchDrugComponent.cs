namespace Posology.Core
{
    public class FrenchDrugComponent:IDrugComponent
    {
        public FrenchDrugComponent(string internalDrugIdentifier)
        {
            InternalDrugIdentifier = internalDrugIdentifier;
        }

        public string InternalDrugIdentifier { get; }
        public string DrugShape { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAmount { get; set; }
        public string ComponentAmountUnit { get; set; }
        public string ComponentType { get; set; }
    }
}