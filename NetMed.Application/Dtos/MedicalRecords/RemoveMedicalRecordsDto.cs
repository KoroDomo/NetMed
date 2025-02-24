
namespace NetMed.Application.Dtos.MedicalRecords
{
    public class RemoveMedicalRecordsDto : DtoBase
    {
        public int Id { get; set; }
        public bool Removed { get; set; }
    }
}
