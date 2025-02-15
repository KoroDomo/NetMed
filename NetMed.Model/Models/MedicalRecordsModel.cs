

namespace NetMed.Model.Models
{
    public class MedicalRecordsModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfVisit { get; set; }

    }
}
