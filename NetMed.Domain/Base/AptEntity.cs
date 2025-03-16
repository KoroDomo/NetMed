
namespace NetMed.Domain.Base
{
    public abstract class AptEntity
    {

       protected AptEntity() 
      { 
            this.CreatedAt = DateTime.UtcNow;
       }
        public  DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;


    }

}
