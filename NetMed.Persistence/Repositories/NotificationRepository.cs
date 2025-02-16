

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
                                     IConfiguration configuration) : base(context)
        {

            this._context = context;
            this._logger = logger;
            this._configuration = configuration;
        }

        public override Task<Notification> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);

        }

        public override Task<OperationResult> SaveEntityAsync(Notification entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<List<Notification>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<OperationResult> UpdateEntityAsync(Notification entity)
        {

            return base.UpdateEntityAsync(entity);
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Notification, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetNotificationsByUserIdAsync(int userId)
        {
            var result = new OperationResult();

            if (userId < 0)
            {
                result.success = false;
                result.Mesagge = "ID no válido.";
                return result;
            }

            try
            {
                var user = await _context.Notifications
                    .FirstOrDefaultAsync(n => n.Id == userId);

                if (user == null)
                {
                    result.success = false;
                    result.Mesagge = "Usuario no encontrado.";
                }
                else
                {
                    result.success = true;
                    result.Mesagge = "Usuarios obtenida con éxito.";
                    result.data = user;
                }
            }
            catch (Exception ex)
            {

                result.success = false;
                result.Mesagge = $"Error al obtener el usuario: {ex.Message}";
                await _context.SaveChangesAsync();
            }
            return result;


        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {
            var result = new OperationResult();

            if (notificationId < 0)
            {
                result.success = false;
                result.Mesagge = "ID no válido.";
                return result;
            }

            try
            {
                var notification = await _context.Notifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId);

                if (notification == null)
                {
                    result.success = false;
                    result.Mesagge = "Notificación no encontrada.";
                }
                else
                {
                    result.success = true;
                    result.Mesagge = "Notificación obtenida con éxito.";
                    result.data = notification;
                }
            }
            catch (Exception ex)
            {

                result.success = false;
                result.Mesagge = $"Error al obtener la notificación: {ex.Message}";
                await _context.SaveChangesAsync();
            }
            return result;

        }
        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "Notification");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.Mesagge = "La notificacion se a creado con exito";

                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Problemas con crear la notificacion";

            }

            return result;

        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "Notification");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Notifications.Update(notification);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.Mesagge = "La notificacion se actualizo con exito";

                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Problemas con actualizar la notificacion";

            }

            return result;
        }

        public async Task<OperationResult> DeleteNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "Notification");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Notifications.Remove(notification);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.Mesagge = "La notificacion se elimino con exito";

                };
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Problemas eliminando la notificacion";

            }

            return result;

        }


    }
}

        
 