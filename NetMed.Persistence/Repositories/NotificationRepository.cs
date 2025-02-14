

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly NetmedContext _context;

         private readonly ILogger<NotificationRepository> _logger;
         private readonly IConfiguration _configuration;

        public NotificationRepository(NetmedContext context,
                                     ILogger < NotificationRepository > logger,
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


    }
}
