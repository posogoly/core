using Posology.Directory;

namespace Posology.Core
{
    public interface IDrugPackaging
    {
        IDrug Drug { get; set; }
        string Description { get; set; }
        string Id { get; set; }
        string Status { get; set; }
        string CommercialisationStatus { get; set; }
        string CommercialisationDate { get; set; }
        string Barcode { get; set; }
        string InternalDrugIdentifier { get; set; }
        IDrugComponents Components { get; set; }

        void AddComponent(IDrugComponent component);
        IDrugComponent GetMainComponent();
    }
}