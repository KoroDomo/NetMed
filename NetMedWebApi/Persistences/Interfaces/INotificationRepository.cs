using NetMedWebApi.Models;
using NetMedWebApi.Models.Notification;

namespace NetMedWebApi.Persistence.Interfaces
{
    public interface INotificationRepository 
    {
        Task<OperationResult<T>> GetNotificationByIdAsync<T>(int Id);

        Task<OperationResultList<NotificationApiModel>> GetAllNotificationAsync();

        Task<OperationResult<SaveNotificationModel>> CreateNotificationAsync(SaveNotificationModel model);

        Task<OperationResult<UpdateNotificationModel>> UpdateNotificationAsync(UpdateNotificationModel model);

        Task<OperationResult<DeleteNotificationModel>> DeleteNotificationAsync(DeleteNotificationModel model);

    }
}
