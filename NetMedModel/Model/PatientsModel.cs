namespace NetMedModel.Model
{
    public class PatientsModel
    {

        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public required string EmergencyContactPhone { get; set; }
        public required string BloodType { get; set; }
        public string? Allergies { get; set; }
        public int? InsuranceProviderID { get; set; } // Nullable por si no tiene seguro
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

}

