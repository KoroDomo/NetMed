
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Persistence.Repositories
{
    public class NotificationRepository :  BaseRepository<Notification,int> , INotificationRepository
    {

        private readonly NetmedContext _context;
        public NotificationRepository(NetmedContext context) : base(context)
        {

        }

        public override Task<OperationResult> SaveEntityAsync(Notification entity)
        {
            return base.SaveEntityAsync(entity);
        }

    }
}
