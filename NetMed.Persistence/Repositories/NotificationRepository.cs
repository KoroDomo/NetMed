using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<NotificationRepository> _logger;
        private readonly JsonMessage _jsonMessage;

        public NotificationRepository(NetmedContext context,
                                     ILogger<NotificationRepository> logger,
                                     JsonMessage messageMapper) : base(context, logger, messageMapper)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = messageMapper;
        }

        public override async Task<OperationResult> SaveEntityAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, _jsonMessage.ErrorMessages["NullEntity"]);

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
            var validationResult = EntityValidator.ValidatePositiveNumber(userId, _jsonMessage.ErrorMessages["InvalidId"]);

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

                if (notifications == null || !notifications.Any())
                {
                    _logger.LogWarning(_jsonMessage.ErrorMessages["NotificationNotFound"], userId);
                    return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NotificationNotFound"], Data = userId };
                }

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
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, _jsonMessage.ErrorMessages["InvalidId"]);

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
            var validationResult = EntityValidator.ValidateNotNull(notification, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationCreated"], nameof(Notification), notification.Id);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["NotificationCreated"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, _jsonMessage.ErrorMessages["NullEntity"]);

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
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, _jsonMessage.ErrorMessages["InvalidId"]);

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

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["NotificationDeleted"], nameof(Notification), notificationId);
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