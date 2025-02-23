using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Entities
{
    [Table("Appointments", Schema = "appointments")]
    public sealed class Appointments : DoctorRelatedEntity
    {
        [Column("AppointmentID")]
        [Key]
        public override int Id { get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
    }
}
