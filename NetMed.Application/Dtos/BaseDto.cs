
namespace NetMed.Application.Dtos
{
   public class BaseDto
   {
     public DateTime createdAt { get; set; } = DateTime.Now;
     public DateTime? updatedAt { get; set; } = DateTime.Now;
   }
}
