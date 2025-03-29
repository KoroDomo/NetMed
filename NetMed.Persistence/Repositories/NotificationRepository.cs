using Azure;
using Microsoft.EntityFrameworkCore;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Model.Models;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.NewFolder;

namespace NetMed.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly NetmedContext _context;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessage;
        private readonly INotificationValidator _notificationValidator;

        public NotificationRepository(NetmedContext context,
                                     ILoggerCustom logger,
                                     JsonMessage messageMapper) : base(context)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = messageMapper;
            _notificationValidator = new NotificationValidator(logger, messageMapper);
        }

        public async override Task<OperationResult> GetAllAsync()
        {
            try
            {
                var network = await _context.Notifications
                    .OrderByDescending(ip => ip.Id)
                    .Select(ip => new NotificationsModel()
                    {
                        NotificationID = ip.Id,
                        Message = ip.Message,
                        SentAt = ip.SentAt,
                        UserID = ip.UserID
                    })
                    .ToListAsync();

                if (!network.Any())
                {
                    _logger.LogError("No se encontro la notication.");
                    return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityCreated"] };

                }
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityCreated"], Data = network };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"], ex.Message);
                throw;
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(Notification notification)
        {
            var validationResult = _notificationValidator.ValidateIsEntityIsNull(notification, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityCreated"], notification.Id);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityCreated"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                throw;
            }
        }

        public async Task<OperationResult> GetNotificationsByUserIdAsync(int userId)
        {

            var validationResult = _notificationValidator.ValidateNumberEntityIsNegative(userId, _jsonMessage.ErrorMessages["InvalidId"]);


            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.UserID == userId)
                    .ToListAsync(); 

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationFound"], nameof(Notification), userId);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["NotificationFound"], Data = notifications };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {

            var validationResult = _notificationValidator.ValidateNumberEntityIsNegative(notificationId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                if (notification == null)
                {
                    _logger.LogWarning(_jsonMessage.ErrorMessages["NotificationNotFound"], notificationId);
                    return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NotificationNotFound"], Data = notificationId };
                }

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationFound"], nameof(Notification), notificationId);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["NotificationFound"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var nullValidationResult = _notificationValidator.ValidateNotificationIsNotNull(notification, _jsonMessage.ErrorMessages["NotificationNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            var idValidationResult = _notificationValidator.ValidateNotificationIdAndUserId(notification.Id, notification.UserID, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!idValidationResult.Success)
            {
                _logger.LogError(idValidationResult.Message);
                return idValidationResult;
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationCreated"],nameof(Notification),notification.Id);

                return new OperationResult{Success = true, Message = _jsonMessage.SuccessMessages["NotificationCreated"], Data = notification};

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult{Success = false,Message = _jsonMessage.ErrorMessages["DatabaseError"]};
            }
        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {

            var nullValidationResult = _notificationValidator.ValidateNotificationIsNotNull(notification, _jsonMessage.ErrorMessages["NotificationNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            var validationResult = _notificationValidator.ValidateNotificationIdAndUserId(notification.Id, notification.UserID, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationUpdated"], nameof(Notification), notification.Id);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["NotificationUpdated"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> DeleteNotificationAsync(int notificationId)
        {
            var nullValidationResult = _notificationValidator.ValidateNumberEntityIsNegative (notificationId, _jsonMessage.ErrorMessages["NotificationNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);


                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationDeleted"], nameof(Notification), notification);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["NotificationDeleted"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
    }
}