using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace NetMed.Domain.Entities
{
    [Table ("Patients", Schema = "users")]
    public sealed class Patients : BaseEntity<int>
    {
        [Column("PatientID")]
        [Key]
        public override int UserID{ get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public string? EmergencyContactName { get; set; }
        [Required]
        public required string EmergencyContactPhone { get; set; }
        [Required]
        public char BloodType { get; set; }
        [Required]
        public string? Allergies { get; set; }
        [Required]
        public int InsuranceProviderID { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}