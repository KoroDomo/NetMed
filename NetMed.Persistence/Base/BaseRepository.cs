using NetMed.Domain.Repository;
using NetMed.Domain.Base;
using System.Linq.Expressions;
using NetMed.Persistence.Context;

namespace NetMed.Persistence.Base
{
    internal class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
        private readonly NetMedContext _context;
        protected BaseRepository(NetMedContext context) 
        {
            _context = context;
        }
        public Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityByIdAsync(TType id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}
