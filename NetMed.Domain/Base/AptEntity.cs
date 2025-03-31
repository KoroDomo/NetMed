
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Base
{
    public abstract class AptEntity
    {
       public AptEntity() 
       {
            
       }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    }

}
