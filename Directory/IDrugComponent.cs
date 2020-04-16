namespace Posology.Core
{
    public interface IDrugComponent
    {
        string InternalDrugIdentifier { get; set; }
        string DrugShape { get; set; }
        string ComponentId { get; set; }
        string ComponentName { get; set; }
        string ComponentAmount { get; set; }
        string ComponentAmountUnit { get; set; }
        string ComponentType { get; set; }
    }
}