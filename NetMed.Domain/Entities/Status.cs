using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("Status", Schema = "system")]
    public sealed class Status : BaseEntity<int>
    {
        [Column("StatusID")]
        [Key]
        public override int Id { get; set; }
        public string StatusName { get; set; }
    }
}