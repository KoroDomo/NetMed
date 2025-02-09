using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class NetworkType : BaseEntity<int>
    {
        [Column("NetworkTypeID")]
        [Key]
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
