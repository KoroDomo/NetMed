using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("Notifications", Schema = "system")]

    public sealed class Notification : BaseEntity<int>
    {
        [Column("NotificationID")]
        [Key]
        public override int Id { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime? SentAt { get; set; }

    }
}
