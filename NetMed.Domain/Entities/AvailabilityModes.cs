using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("AvailabilityModes", Schema = "medical")]
    public sealed class AvailabilityModes : BaseEntity<short> 
    {
        [Column("SAvailabilityModeID")]
        [Key]
        public override short Id { get; set; }
        [Required]
        public required string AvailabilityModeName { get; set; }

        // Metodo para activar el modo de disponibilidad
        public void Activate()
        {
            IsActive = true;
        }

        // Metodo para desactivar el modo de disponibilidad
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
