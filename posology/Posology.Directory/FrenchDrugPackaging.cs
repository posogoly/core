using System.Collections.Generic;

namespace Posology.Core
{
    public class FrenchDrugPackaging: IDrugPackaging
    {
        public FrenchDrugPackaging(IDrug drug)
        {
            Drug = drug;
            InternalDrugIdentifier = drug.InternalIdentifier;
        }

        public string InternalDrugIdentifier { get; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string CommercialisationStatus { get; set; }
        public string CommercialisationDate { get; set; }
        public string Barcode { get; set; }
        public IDrug Drug { get; }
        public List<IDrugComponent> Components { get; }
    }
}