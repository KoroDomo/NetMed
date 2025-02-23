
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Base
{
    public abstract class AptEntity
    {

       public AptEntity() 
       { 
           this.CreatedAt = DateTime.Now;
           this.UpdatedAt = DateTime.Now;
       }
        [NotMapped]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public bool IsActive { get; set; } = true;


    }

}
