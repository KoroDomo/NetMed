using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class Specialties : BaseEntity<int>
    {
        [Column("SpecialtyID")]
        [Key]
        public override int Id { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }

        // Metodo para activar la especialidad
        public void Activate()
        {
            IsActive = true;
        }

        // Metodo para desactivar la especialidad
        public void Deactivate()
        {
            IsActive = false;
        }
        
    }
}
