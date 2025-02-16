

namespace NetMed.Model.Models
{
    public class MedicalRecordsModel
    {
        public required int RecordID { get; set; }
        public required int PatientId { get; set; }
        public required int DoctorID { get; set; }
        public required string Diagnosis { get; set; }
        public required string Treatment { get; set; }
        public required DateTime DateOfVisit { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
