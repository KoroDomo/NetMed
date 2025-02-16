

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
            OperationResult result = new OperationResult();

            if (userId < 0)
            {
                result.success = false;
                result.Mesagge = "El usuario no es valido";
            }
            else
            { 
            
             result.success = true;
             result.Mesagge = "La notificacion fue obtenida correctamente";
            }
            return result;
        
        }

        public async Task<OperationResult> GetNotificationByIdAsync(int notificationId)
        {
            var result = new OperationResult();

            if (notificationId < 0)
            {
                result.success = false;
                result.Mesagge = "ID no valido";
            }
            else 
            { 
             result.success = true;
             result.Mesagge = "Notificacion obtenda con exito";
  
            }
            return result;

        }
        public async Task<OperationResult> CreateNotificationAsync(Notification notification)
        {
            var validationResult =  EntityValidator.ValidateNotNull(notification, "Notification");


            if (!validationResult.success)
            { 
             return validationResult;
            
            }

            var result = new OperationResult();
            { 
             result.success = true;
             result.Mesagge = "La notificacion fue creada con exito";
            
            };

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
            {
                result.success = true;
                result.Mesagge = "La notificacion actualizada  con exito";

            };

            return result;




        }

        public async Task<OperationResult> DeleteNotificationAsync(int notificationId)
        {
            var validationResult = EntityValidator.ValidateNotNull(notificationId, "Notification");

            var  result = new OperationResult();
            if (notificationId < 0)
            {
                result.success = false;
                result.Mesagge = "El ID no es valido";

            }
            else
            {
                result.success = true;
                result.Mesagge = "El mensaje se elimino con exito";

            }

            return result;

        }
    }
}

