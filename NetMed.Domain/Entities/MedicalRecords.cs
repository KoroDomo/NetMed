

using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("MedicalRecords", Schema = "medical")]
    public sealed class MedicalRecords : DoctorRelatedEntity
    {
        [Column("RecordID")]
        [Key]
        public override int Id { get; set; }
        [Required]
        public required int PatientID { get; set; }
        [Required]
        public required string Diagnosis { get; set; }
        [Required]
        public required string Treatment { get; set; }
        [Required]
        public required DateTime DateOfVisit { get; set; }

        // Metodo para actualizar el diagnostico
        public void UpdateDiagnosis(string newDiagnosis)
        {
            Diagnosis = newDiagnosis;
            UpdatedAt = DateTime.Now;
        }

        // Metodo para actualizar el tratamiento
        public void UpdateTreatment(string newTreatment)
        {
            Treatment = newTreatment;
            UpdatedAt = DateTime.Now;
        }
    }
}
