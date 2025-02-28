using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("Doctors", Schema = "users")]
    public sealed class Doctors : BaseEntity<int>
    {


        [Column("DoctorID")]
        [Key]

        public override int UserID { get ; set ; }
        [Required]
        public short  SpecialtyID { get; set; }

        [Required]
        public required string LicenseNumber { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public int YearsOfExperience { get; set; }
        [Required]
        public required string Education { get; set; }
        [Required]
        public string? Bio { get; set; }
        [Required]
        public decimal? ConsultationFee { get; set; }
        [Required]
        public required string ClinicAddress { get; set; }
        [Required]
        public DateOnly LicenseExpirationDate { get; set; }
    }
}