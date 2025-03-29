
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Validator.Interfaces;

namespace NetMed.Infraestructure.Validator.Interfaz
{
    public interface INotificationValidator : IBaseValidator<Notification>
    {
        OperationResult ValidateNotificationIsNotNull(Notification notification, string erroMessage);

        OperationResult ValidateNotificationIdAndUserId(int ID,int UserId, string erroMessage);

        OperationResult ValidateNotificationMessage(Notification notification);

        OperationResult ValidateSentAt(DateTime? sentAt);

        OperationResult ValidateNotification(Notification notification);


    }
}
