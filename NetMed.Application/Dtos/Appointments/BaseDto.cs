namespace NetMed.Application.Dtos.Appointments
{
   public class BaseDto
   {
     public DateTime createdAt { get; set; } = DateTime.Now;
     public DateTime? updatedAt { get; set; } = DateTime.Now;
   }
}
