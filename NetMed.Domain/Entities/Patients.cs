using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class Patients : PersonEntity
    {
        [Column("PatientID")]
        [Key]
        public override int Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public required string EmergencyContactPhone { get; set; }
        public byte BloodType { get; set; }
        public string? Allergies { get; set; }
        public int InsuranceProviderID { get; set; }
    }
}