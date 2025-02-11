using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using System.Linq.Expressions;
using NetMed.Persistence.Context;
using Microsoft.EntityFrameworkCore;



namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
        private readonly NetMedContext _context;
        private DbSet<TEntity> Entity { get; set; } // Investigar. El profesor tiene private TEntity Entity { get; set; } (Puede ser debido a la verison del Entity Framework
        protected BaseRepository(NetMedContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }
        public Task<TEntity> GetEntityByIdAsync(TType id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task SaveEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().CountAsync(filter) > 0;
        }
    }


}
