using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table ("Specialties", Schema ="medical")]
    public sealed class Specialties : BaseEntity<short>
    {
        [Column("SpecialtyID")]
        [Key]
        public override short Id { get; set; }
        [Required]
        public required string SpecialtyName { get; set; }

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
