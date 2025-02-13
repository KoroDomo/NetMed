using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories.Interfaces;


namespace NetMed.Persistence.Repositories
{
    internal class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        private readonly NetMedContext _context;
        public UsersRepository(NetMedContext context) : base(context)
        {
            _context = context;
        }
    }
}