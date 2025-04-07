
using NetMedWebApi.Application.Base;
using NetMedWebApi.Models.Notification;

namespace NetMedWebApi.Application.Contracts
{
    public interface INotificationContract : IBaseContract<NotificationApiModel, SaveNotificationModel, UpdateNotificationModel, DeleteNotificationModel>
    {

    }
}
