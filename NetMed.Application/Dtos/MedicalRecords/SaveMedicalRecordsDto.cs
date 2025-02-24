

namespace NetMed.Application.Dtos.MedicalRecords
{
    public  class SaveMedicalRecordsDto : MedicalRecordsDtos
    {
        public int PatientId { get; set; }
        public int DoctorID { get; set; }
        public DateTime DateOfVisit { get; set; }
    }
}
