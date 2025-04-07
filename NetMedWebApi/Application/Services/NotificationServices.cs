using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Notification;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Application.Services
{
    public class NotificationServices : INotificationContract
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILoggerCustom _logger;
        private readonly IJsonMessage _message;

        public NotificationServices(INotificationRepository notification, ILoggerCustom logger, IJsonMessage message)
        {
            _notificationRepository = notification;
            _logger = logger;
            _message = message;

        }

        public async Task<OperationResult<SaveNotificationModel>> Create(SaveNotificationModel dto)
        {
            var result = new OperationResult<SaveNotificationModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _notificationRepository.CreateNotificationAsync(dto);

                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<DeleteNotificationModel>> Delete(DeleteNotificationModel dto)
        {
            var result = new OperationResult<DeleteNotificationModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
              
                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }

                _logger.LogWarning(result.Message);
                result = await _notificationRepository.DeleteNotificationAsync(dto);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResultList<NotificationApiModel>> GetAll()
        {
            var result = new OperationResultList<NotificationApiModel>();

            try
            {
                var repositoryResult = await _notificationRepository.GetAllNotificationAsync();

                if (!repositoryResult.Success)
                {
                    result.Message = _message.ErrorMessages["NullEntity"];
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogWarning(result.Message);
                return repositoryResult;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GetAllError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<T>> GetById<T>(int Id)
        {
            var result = new OperationResult<T>();

            try
            {
                 result = await _notificationRepository.GetNotificationByIdAsync<T>(Id);
                if (!result.Success)
                {
                    result.Message = _message.ErrorMessages["NullEntity"];
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogWarning(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GetAllError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<UpdateNotificationModel>> Update(UpdateNotificationModel dto)
        {
            var result = new OperationResult<UpdateNotificationModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _notificationRepository.UpdateNotificationAsync(dto);

                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
