using System.ComponentModel.DataAnnotations.Schema;
namespace NetMed.Domain.Base
{
    public abstract class AptEntity 
    {

       protected AptEntity() 
       { 
            this.CreatedAt = DateTime.Now;
       }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
         public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        


    }

}
