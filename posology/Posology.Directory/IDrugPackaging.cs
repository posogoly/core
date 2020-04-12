using System.Collections.Generic;

namespace Posology.Core
{
    public interface IDrugPackaging
    {
        IDrug Drug { get; }
        string Description { get; set; }
        string Id { get; set; }
        string Status { get; set; }
        string CommercialisationStatus { get; set; }
        string CommercialisationDate { get; set; }
        string Barcode { get; set; }
        string InternalDrugIdentifier { get; }
        List<IDrugComponent> Components { get; }
    }
}