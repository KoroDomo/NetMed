namespace NetMedModel.Model
{
    public class DoctorsModel
    {
 
        public int SpecialtyID { get; set; }
        public required string LicenseNumber { get; set; }
        public required string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public required string Education { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public required string ClinicAddress { get; set; }
        public required int AvailabilityModeId { get; set; }
        public DateOnly LicenseExpirationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
