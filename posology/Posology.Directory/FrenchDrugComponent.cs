namespace Posology.Core
{
    public class FrenchDrugComponent:IDrugComponent
    {
        public FrenchDrugComponent(string internalDrugIdentifier)
        {
            InternalDrugIdentifier = internalDrugIdentifier;
        }

        public string InternalDrugIdentifier { get; }
        public string Denomination { get; set; }
        public string DrugShape { get; set; }
        public string AutorisationStatus { get; set; }
        public string AdministrationType { get; set; }
        public string UnkownNumber { get; set; }
        public string CompositionId { get; set; }
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentAmount { get; set; }
        public string ComponentAmountUnit { get; set; }
        public string ComponentType { get; set; }
    }
}