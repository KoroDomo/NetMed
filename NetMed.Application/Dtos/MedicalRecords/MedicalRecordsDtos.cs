namespace NetMed.Application.Dtos.MedicalRecords
{
    public class MedicalRecordsDtos : DtoBase
    {
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int PatientID { get; set; }
    }
}
