
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Context.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {   
            Task <OperationResult> GetNotificationsByUserIdAsync(int userId);

            Task<OperationResult> GetNotificationByIdAsync(int notificationId);

            Task<OperationResult> CreateNotificationAsync(Notification notification);

            Task<OperationResult> UpdateNotificationAsync(Notification notification);

            Task<OperationResult> DeleteNotificationAsync(int notification);


    }
}
