using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class AvailabilityModes : BaseEntity<int>
    {
        [Column("SAvailabilityModeID")]
        [Key]
        public override int Id { get; set; }
        public string AvailabilityModeName { get; set; }
    }
}
