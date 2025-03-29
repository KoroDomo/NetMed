using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Persistence.NewFolder
{
    public static class NotificacionRepositoryMapper
    {
        public static IQueryable<NotificationsModel> MapToNotificationModel(
            this IQueryable<Notification> query)
        {
            return query.Select(ip => new NotificationsModel()
            {
                NotificationID = ip.Id,
                Message = ip.Message,
                SentAt = ip.SentAt,
                UserID = ip.UserID
            });
        }
    }

}
