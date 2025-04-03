using NetMed.Application.Dtos.Notification;
using NetMed.Domain.Entities;

namespace NetMed.Application.Mapper
{
    public static class NotificationMapper
    {
        public static NotificationDto ToDto(Notification notification)
        {
            return new NotificationDto
            {
                id = notification.Id,
                UserID = notification.UserID,
                Message = notification.Message,
                SentAt = notification.SentAt
            };
        }

        public static List<NotificationDto> ToDtoList(IEnumerable<Notification> notifications)
        {
            return notifications.Select(n => ToDto(n)).ToList();
        }

        public static Notification ToEntity(SaveNotificationDto dto)
        {
            return new Notification
            {
                UserID = dto.UserID,
                Message = dto.Message,
                SentAt = dto.SentAt ?? DateTime.UtcNow
            };
        }

        public static Notification ToEntity(UpdateNotificationDto dto)
        {
            return new Notification
            {
                Id = dto.id,
                UserID = dto.UserID,
                Message = dto.Message,
                SentAt = dto.SentAt,
            };
        }

        public static Notification ToEntity(DeleteNotificationDto dto)
        {
            return new Notification
            {
                Id = dto.NotificationId,
               
            };
        }

        public static void UpdateEntity(this Notification notification, UpdateNotificationDto dto)
        {
            notification.UserID = dto.UserID;
            notification.Message = dto.Message;
            notification.SentAt = dto.SentAt;
        }
    }
}