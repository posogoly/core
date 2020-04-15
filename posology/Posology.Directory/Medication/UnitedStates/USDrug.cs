using Posology.Core;

namespace Posology.Directory.Medication.UnitedStates
{
    public class USDrug : IDrug
    {
        public string InternalIdentifier { get; set; }
        public string Denomination { get; set; }
        public string AutorisationStatus { get; set; }
        public string AdministrationType { get; set; }
        public string NoticeDocumentId { get; set; }
        public string DrugType { get; set; }
    }
}