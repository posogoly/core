using Posology.Core;

namespace Posology.Directory
{
    public interface IDrug
    {
        //todo add side effects
        string InternalIdentifier { get; set; }
        string Denomination { get; set; }
        string AutorisationStatus { get; set; }
        string AdministrationType { get; set; }
        string NoticeDocumentId { get; set; }
        string DrugType { get; set; }
        IDrugComponents Components { get; set; }

        void AddComponent(IDrugComponent component);
        IDrugComponent GetMainComponent();
    }
}