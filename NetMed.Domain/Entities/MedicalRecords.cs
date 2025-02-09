

using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class MedicalRecords : DoctorRelatedEntity
    {
        [Column("RecordID")]
        [Key]
        public override int Id { get; set; }
        public int PatientID { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
    }
}
