using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class Doctors : BaseEntity<int>
    {
        [Column("DoctorID")]
        [Key]
        public override int Id { get; set; }
        public int SpecialtyID { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public string Education { get; set; }
        public string Bio { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string ClinicAddress { get; set; }
        public int AvailabilityModelID { get; set; }
        public DateOnly LicenseExpirationDate { get; set; }
    }
}