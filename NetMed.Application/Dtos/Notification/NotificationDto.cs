
namespace NetMed.Application.Dtos.Notification
{
    public class NotificationDto : DtoBase
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public required string Message { get; set; }
        public DateTime? SentAt { get; set; }



    }
}
