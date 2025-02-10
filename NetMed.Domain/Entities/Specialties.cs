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
    }
}
