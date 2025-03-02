
namespace NetMed.Application.Dtos
{
   public class BaseDto
   {
     public DateTime CreatedAt { get; set; } = DateTime.Now;
     public DateTime? UpdatedAt { get; set; } = DateTime.Now;
   }
}
