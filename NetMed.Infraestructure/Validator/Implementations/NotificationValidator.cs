

using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Infraestructure.Validator.Base;
using NetMed.Infraestructure.Validator.Interfaz;

namespace NetMed.Infraestructure.Validator.Implementations
{
    public class NotificationValidator : BaseValidator, INotificationValidator
    {
        private readonly JsonMessage _jsonMessage;
        private readonly ILoggerCustom _loggerCustom;
        private readonly IBaseRepository<Notification> _notificationRepository;


        public NotificationValidator(ILoggerCustom loggerCustom,
                                     JsonMessage messageMapper) : base(loggerCustom, messageMapper)
        {

            _loggerCustom = loggerCustom;
            _jsonMessage = messageMapper;

        }

        public OperationResult ValidateIsEntityIsNull(Notification entity, string errorMessage)
        {
            OperationResult result = new OperationResult();

            if (entity == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NullEntity"] };

            }
            else
            {
                return new OperationResult { Success = true };

            }

        }

        public OperationResult ValidateNotificationIsNotNull(Notification notification, string erroMessage)
        {
            OperationResult result = new OperationResult();

            if (notification == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["RolesNull"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }


        }

        public OperationResult ValidateNotificationIdAndUserId(int notificationId, int userId, string errorMessage)
        {
            OperationResult result = new OperationResult();

            if (notificationId < 0 || userId < 0)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };

            }
            else
            {

                return new OperationResult { Success = true };

            }

        }

        public OperationResult ValidateSentAt(DateTime? sentAt)
        {
            OperationResult result = new OperationResult();

            if (sentAt.HasValue && sentAt.Value > DateTime.UtcNow)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NotificationSentAt"] };
           
            }
            else
            {
                return new OperationResult { Success = true };
            }

        }

        public OperationResult ValidateNotificationMessage(Notification notification)
        {
            if (notification.Message.Length > 600)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NotificationMessage"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }

       
    }
}

