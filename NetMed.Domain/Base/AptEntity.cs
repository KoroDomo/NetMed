
namespace NetMed.Domain.Base
{
    public abstract class AptEntity
    {

       protected AptEntity() 
       { 
            this.CreatedAt = DateTime.Now;
       }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;


    }

}
