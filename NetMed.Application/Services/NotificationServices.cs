using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Notification;
using NetMed.Application.Interfaces;
using NetMed.Application.Mapper;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class NotificationServices : INotificationContract
    {
        private readonly NetmedContext _context;
        private readonly INotificationRepository _notificationRepository;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessageMapper;
        private readonly INotificationValidator _notificationValidator;

        public NotificationServices(
            NetmedContext context,
            INotificationRepository notificationRepository,
            ILoggerCustom logger,
            JsonMessage jsonMessageMapper)
        {
            _context = context;
            _logger = logger;
            _notificationRepository = notificationRepository;
            _jsonMessageMapper = jsonMessageMapper;
            _notificationValidator = new NotificationValidator(logger, jsonMessageMapper);
        }

        public async Task<OperationResult> GetAllDto()
        {

            var result = new OperationResult();
            try
            {
                var repositoryResult = await _notificationRepository.GetAllAsync();
                if (!repositoryResult.Success || repositoryResult.Data == null)
                {
                    _logger.LogInformation(_jsonMessageMapper.ErrorMessages["GetAllEntity"], "Notification");
                    result.Success = false;
                    result.Message = _jsonMessageMapper.ErrorMessages["GetAllEntity"];
                    return result;
                }

                var notification = (IEnumerable<Notification>)repositoryResult.Data;
                var roleDtos = NotificationMapper.ToDtoList(notification);

                result.Success = true;
                result.Message = _jsonMessageMapper.SuccessMessages["GetAllEntity"];
                result.Data = roleDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessageMapper.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> GetDtoById(int Id)
        {
            var result = new OperationResult();
            try
            {
                var notification = await _notificationRepository.GetNotificationByIdAsync(Id);
                var notificationDto = NotificationMapper.ToDto(notification.Data);
                return result.Data = notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult {Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"]};
            }
           
        }
        

        public async Task<OperationResult> SaveDto(SaveNotificationDto dtoSave)
        {
            var result = new OperationResult();
            try
            {
                var notification = new Notification
                {
                    UserID = dtoSave.UserID,
                    Message = dtoSave.Message,
                    SentAt = dtoSave.SentAt
                };

                var validationResult = _notificationValidator.ValidateNotificationIsNotNull(notification, _jsonMessageMapper.ErrorMessages["NullEntity"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _notificationRepository.CreateNotificationAsync(notification);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var savedNotification = (Notification)repositoryResult.Data;
                var notificationDto = NotificationMapper.ToDto(savedNotification);

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationCreated"], nameof(Notification), savedNotification.Id);
                result.Success = true;
                result.Message = _jsonMessageMapper.SuccessMessages["NotificationCreated"];
                result.Data = notificationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessageMapper.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> UpdateDto(UpdateNotificationDto dtoUpdate)
        {
            var result = new OperationResult();
            try
            {
                var validationResult = _notificationValidator.ValidateNumberEntityIsNegative(dtoUpdate.id, _jsonMessageMapper.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var notification = new Notification
                {
                    Id = dtoUpdate.id,
                    UserID = dtoUpdate.UserID,
                    Message = dtoUpdate.Message,
                    SentAt = dtoUpdate.SentAt,
                };

                var repositoryResult = await _notificationRepository.UpdateNotificationAsync(notification);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var updatedNotification = (Notification)repositoryResult.Data;
                var notificationDto = NotificationMapper.ToDto(updatedNotification);

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationUpdated"], nameof(Notification), updatedNotification.Id);
                result.Success = true;
                result.Message = _jsonMessageMapper.SuccessMessages["NotificationUpdated"];
                result.Data = notificationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessageMapper.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> DeleteDto(int notificationId)
        {
            var result = new OperationResult();
            try
            {
                var repositoryResult = await _notificationRepository.DeleteNotificationAsync(notificationId);
                if (!repositoryResult.Success)
                {
                    _logger.LogError(repositoryResult.Message);
                    return repositoryResult;
                }

                var validationResult = _notificationValidator.ValidateNumberEntityIsNegative(notificationId, _jsonMessageMapper.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationDeleted"], nameof(Notification));
                result.Success = true;
                result.Message = _jsonMessageMapper.SuccessMessages["NotificationDeleted"];
                result.Data = notificationId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessageMapper.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> GetNotificationsByUserIdAsync(int userId)
        {
            var result = new OperationResult();
            try
            {
                var validationResult = _notificationValidator.ValidateNumberEntityIsNegative(userId, _jsonMessageMapper.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
                if (!repositoryResult.Success || repositoryResult.Data == null)
                {
                    result.Success = false;
                    result.Message = _jsonMessageMapper.ErrorMessages["NotificationNotFound"];
                    return result;
                }

                var notifications = (IEnumerable<Notification>)repositoryResult.Data;
                var notificationDtos = NotificationMapper.ToDtoList(notifications);

                result.Success = true;
                result.Message = _jsonMessageMapper.SuccessMessages["NotificationFound"];
                result.Data = notificationDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessageMapper.ErrorMessages["DatabaseError"];
            }
            return result;
        }
    }
}