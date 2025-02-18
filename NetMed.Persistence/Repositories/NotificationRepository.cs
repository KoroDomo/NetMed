

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
            var validationResult = EntityValidator.ValidatePositiveNumber(userId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var notification = await _context.Notifications.FindAsync(userId);

                var notNullNotification = EntityValidator.ValidateNotNull(notification, "La notificacion no a sido encontrada");


                if (!notNullNotification.Success)
                {

                    return notNullNotification;

                }
                else
                {
                    return new OperationResult { Success = true, Mesagge = "Notificación obtenida con éxito", Data = notification };

                }
            }
            catch (Exception ex)
            {
                await _context.SaveChangesAsync();
                return new OperationResult { Success = false, Mesagge = "Error al obtener la notificación con el usuario" };

            }


        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                var notNullNotification = EntityValidator.ValidateNotNull(notificationId, "La notificacion no a sido encontrada");

 
                if (!notNullNotification.Success)
                {

                    return notNullNotification;
   
                }
                else
                {
                    return new OperationResult {Success = true, Mesagge = "Notificación obtenida con éxito", Data = notificationId };

                }
            }
            catch (Exception ex)
            {
                await _context.SaveChangesAsync();
                return new OperationResult { Success = false, Mesagge = "Error al obtener la notificación" };
              
            }
        }
        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "LA notificacion no puede ser creada");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();

                    return new OperationResult { Success = true, Mesagge = "La notificacion se a creado con exito" };


                };
            }
            catch (Exception ex)
            {

                return new OperationResult { Success = false, Mesagge = "Problemas con crear la notificacion" };

            }

        }

        public async Task<OperationResult> UpdateNotificationAsync(Notification notification)
        {
            var validationResult = EntityValidator.ValidateNotNull(notification, "La notificacion no se a encontrado");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.Notifications.Update(notification);
                    await _context.SaveChangesAsync();

                    return new OperationResult {Success = true, Mesagge = "La notificacion se actualizo con exito" };

                };
            }
            catch (Exception ex)
            {

                return new OperationResult {Success = false, Mesagge = "Problemas con actualizar la notificacion" };
              
            }

            
        }

        public async Task<OperationResult> DeleteNotificationAsync(int notificationId)
        {

            var validationResult = EntityValidator.ValidatePositiveNumber(notificationId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var notification = await _context.Notifications.FindAsync(notificationId);

                var notNullNotification = EntityValidator.ValidateNotNull(notification, "La notificacion no a sido encontrada");

                if (!notNullNotification.Success)
                {
                    return notNullNotification;
                };

                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Mesagge = "Mensaje eliminado con exito" };

            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Mesagge = "Surgieron problemas a la hora de eliminar la notificacion"

                };
            }
        }
    }
} 