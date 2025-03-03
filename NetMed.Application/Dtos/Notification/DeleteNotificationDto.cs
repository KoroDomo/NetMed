

namespace NetMed.Application.Dtos.Notification
{
    public class DeleteNotificationDto : DtoBase
    {
        public int NotificationId { get; set; }

        public bool Deleted { get; set; }

    }
}
