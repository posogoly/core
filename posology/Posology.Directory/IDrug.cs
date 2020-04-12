namespace Posology.Core
{
    public interface IDrug
    {
        string InternalIdentifier { get; set; }
        string Denomination { get; set; }
        string AutorisationStatus { get; set; }
        string AdministrationType { get; set; }
        string UnkownNumber { get; set; }
        string DrugType { get; set; }
    }
}