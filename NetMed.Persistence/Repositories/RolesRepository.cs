
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Persistence.Repositories
{
   public class RolesRepository : BaseRepository<Roles,int>, IRolesRepository
    {

        private readonly NetmedContext _context;
        public RolesRepository(NetmedContext context) : base(context)
        {

        }

        public override Task<OperationResult> SaveEntityAsync(Roles entity)
        {
            return base.SaveEntityAsync(entity);
        }


    }
}
