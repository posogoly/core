namespace Posology.Core
{
    public interface IDrug
    {
        string InternalIdentifier { get; set; }
        string Denomination { get; set; }
    }
}