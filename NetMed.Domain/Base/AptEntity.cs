
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Base
{
    public abstract class AptEntity
    {

       protected AptEntity() 
       { 
            this.CreatedAt = DateTime.Now;
       }
        [NotMapped]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public bool IsActive { get; set; } = true;


    }

}
