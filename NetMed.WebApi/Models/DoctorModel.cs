namespace NetMed.Web.Models
{
    public class DoctorModel
    {
        public int doctorId { get; set; }
        public int specialtyID { get; set; }
        public  string? licenseNumber { get; set; }
        public  string? phoneNumber { get; set; }
        public int yearsOfExperience { get; set; }
        public  string? education { get; set; }
        public string? bio { get; set; }
        public decimal consultationFee { get; set; }
        public  string? clinicAddress { get; set; }
        public  int availabilityModeId { get; set; }
        public DateOnly licenseExpirationDate { get; set; }
    }
}
