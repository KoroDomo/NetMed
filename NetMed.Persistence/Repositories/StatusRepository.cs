using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Persistence.Repositories
{
     public  class StatusRepository : BaseRepository<Status,int>, IStatusRepository
    {
        private readonly NetmedContext _context;
        public StatusRepository(NetmedContext context) : base(context)
        {
        
        }

        public override Task<OperationResult> SaveEntityAsync(Status entity)
        {
            return base.SaveEntityAsync(entity);
        }

        //public override Task<OperationResult> Get

    }
}
