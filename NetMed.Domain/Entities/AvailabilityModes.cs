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
        public DateTime DateOfVisit { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }

        // Método para activar el modo de disponibilidad
        public void Activate()
        {
            IsActive = true;
        }

        // Método para desactivar el modo de disponibilidad
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
