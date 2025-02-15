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
