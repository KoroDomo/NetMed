using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Notification;
using NetMed.Application.Interfaces;
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

        public  NotificationServices(NetmedContext context,INotificationRepository notificationRepository,
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
            OperationResult result = new OperationResult();

            try
            {
                var notification = await _notificationRepository.GetAllAsync();
                _logger.LogInformation(_jsonMessageMapper.ErrorMessages["GetAllEntity"], notification);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["GetAllEntity"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }

            
        }

        public async Task<OperationResult> GetDtoById(int notification)
        {
            OperationResult result = new OperationResult();

            try
            {
                result = _notificationValidator.ValidateNumberEntityIsNegative(notification, _jsonMessageMapper.ErrorMessages["InvalidId"]);

                if (!result.Success)
                {
                    _logger.LogError(result.Message);
                    return result;
                }

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["GetAllEntity"], nameof(Notification));
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["NotificationFound"]};

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };

            }

        }

        public async Task<OperationResult> SaveDto(SaveNotificationDto dtoSave)
        {
          OperationResult result = new OperationResult();
            try
            {               
                var notification = new Notification
                {
                   
                    UserID = dtoSave.UserID,
                    Message = dtoSave.Message,
                    SentAt = dtoSave.SentAt,
                };

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationCreated"], nameof(Notification), notification);
                var notificationSave = await _notificationRepository.SaveEntityAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
            return result;

        }

        public async Task<OperationResult> UpdateDto(UpdateNotificationDto dtoUpdate)
        {
            OperationResult result = new OperationResult();
            try
            {
                var notification = new Notification
                {
                    Id = dtoUpdate.NotificationId,
                    UserID = dtoUpdate.UserID,
                    Message = dtoUpdate.Message,
                    SentAt = dtoUpdate.SentAt,
                };

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationUpdated"], nameof(Notification), notification);
                var notificationUp = await _notificationRepository.UpdateNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
            return result;
        }
      

        public async Task<OperationResult> DeleteDto(int dtoDelete)
        {
            var notification = new Notification
            {
                Id = dtoDelete
               
            };

            try
            {
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["NotificationDeleted"], nameof(Notification));
                var notificationUp = await _notificationRepository.DeleteNotificationAsync(notification.Id);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["NotificationDeleted"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

       
    }
}