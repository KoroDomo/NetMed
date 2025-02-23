

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<NotificationRepository> _logger;
        private readonly IConfiguration _configuration;

        public NotificationRepository(NetmedContext context,
                                     ILogger<NotificationRepository> logger,
                                     IConfiguration configuration) : base(context,logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetNotificationsByUserIdAsync(int userId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(userId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "userId", userId);
                return validationResult;
            }

            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.UserID == userId)
                    .ToListAsync();

                if (notifications == null || !notifications.Any())
                {
                    _logger.LogWarning(_configuration["ErrorMessages:NotificationNotFound"], "userId", userId);
                    return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:NotificationNotFound"] };
                }

                _logger.LogInformation(_configuration["ErrorMessages:NotificationRetrieved"], "userId", userId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:NotificationRetrieved"], Data = notifications };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "notificationId", notificationId);
                return validationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                if (notification == null)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:NotificationNotFound"], "notificationId", notificationId);
                    return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:NotificationNotFound"] };
                }

                _logger.LogInformation(_configuration["ErrorMessages:NotificationRetrieved"], "notificationId", notificationId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:NotificationRetrieved"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "notification", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:NotificationCreated"], "notificationId", notification.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:NotificationCreated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "notification", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:NotificationUpdated"], "notificationId", notification.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:NotificationUpdated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> DeleteNotificationAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "notificationId", notificationId);
                return validationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                if (notification == null)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:NotificationNotFound"], "notificationId", notificationId);
                    return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:NotificationNotFound"] };
                }

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:NotificationDeleted"], "notificationId", notificationId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:NotificationDeleted"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }
    }
} 