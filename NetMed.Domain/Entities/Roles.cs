using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    [Table("Roles", Schema = "system")]
    public sealed class Roles : BaseEntity<int>
    {
        [Column("RoleID")]
        [Key]
        public override int Id { get; set; }
        public string RoleName { get; set; }


    }
}
