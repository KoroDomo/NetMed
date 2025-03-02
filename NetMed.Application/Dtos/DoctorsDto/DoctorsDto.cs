

namespace NetMed.Application.Dtos.Doctors
{
    public class DoctorsDto : DtoBase
    {
        public short SpecialtyID { get; set; }
        public required string LicenseNumber { get; set; }

        public required string PhoneNumber { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal? ConsultationFee { get; set; }

        public required string ClinicAddress { get; set; }

        public required string Education { get; set; }
        public string? Bio { get; set; } = null;
        public DateOnly LicenseExpirationDate
        {
            get; set;

        }
    }
}
