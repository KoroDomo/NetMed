

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
        private readonly IConfiguration _configuration;

        public NotificationRepository(NetmedContext context,
                                     ILogger<NotificationRepository> logger,
                                     IConfiguration configuration) : base(context,logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "La entidad Notificaciones no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Notificación creada con éxito", Data = notification };
            }
            catch (Exception ex)
            {
                return new OperationResult
                { Success = false, Message = "Error con la base de datos" };
            }
        }



        public async Task<OperationResult> GetNotificationsByUserIdAsync(int userId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(userId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.UserID == userId)
                    .ToListAsync();

                if (notifications == null || !notifications.Any())
                {
                    return new OperationResult { Success = false, Message = "UserId no valido", Data = userId };
                }

                return new OperationResult { Success = true, Message = "Notificaciones encontradas con exito", Data = notifications };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error en la base de datos" };
            }
        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                if (notification == null)
                {
                    return new OperationResult { Success = false, Message = "ID no valido" };
                }

                return new OperationResult { Success = true, Message = "Notificacion encontrada con exito", Data = notification };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error con la base de datos"};
            }
        }

        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "La entidad Notificaciones no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Notificación creada con éxito", Data = notification };
            }
            catch (Exception ex)
            {
                return new OperationResult
                { Success = false, Message = "Error con la base de datos" };
            }
        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "La entidad no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Notificación actualizada con éxito", Data = notification };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error con la base de datos" };
            }
        }

        public async Task<OperationResult> DeleteNotificationAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                if (notification == null)
                {
                    return new OperationResult { Success = false, Message = "No se encontró la notificación", Data = notification };
                }

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Notificación eliminada con éxito",Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = "Error con la base de datos" };
            }
        }
    }
} 