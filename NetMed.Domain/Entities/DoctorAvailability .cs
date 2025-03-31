using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("DoctorAvailability", Schema = "appointments")]
    public sealed class DoctorAvailability : BaseEntityDoctor<int> //DoctorRelatedEntity
    {
        [Column("AvailabilityID")]
        [Key]
        public override int Id { get; set; }
        public int DoctorID { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
