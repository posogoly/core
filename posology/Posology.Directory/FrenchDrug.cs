namespace Posology.Core
{
    public class FrenchDrug: IDrug
    {
        public string InternalIdentifier { get; set; }
        public string Denomination { get; set; }
        public string AutorisationHolder { get; set; }
        public string HeldSince { get; set; }
        public string AutorisedSince { get; set; }
        public string AutorisationStatus { get; set; }
        public string PrescriptionList { get; set; }
        public string AdministrationType { get; set; }
        public string PrescriptionDetails { get; set; }
        public string PackagingDetails { get; set; }
        public string DrugType { get; internal set; }
        public string UnkownNumber { get; internal set; }
    }
}