namespace Posology.Core
{
    public interface IDrugComponent
    {
        string InternalDrugIdentifier { get; }
        string Denomination { get; set; }
        string DrugShape { get; set; }
        string AutorisationStatus { get; set; }
        string AdministrationType { get; set; }
        string UnkownNumber { get; set; }
        string CompositionId { get; set; }
        string ComponentId { get; set; }
        string ComponentName { get; set; }
        string ComponentAmount { get; set; }
        string ComponentAmountUnit { get; set; }
        string ComponentType { get; set; }
    }
}