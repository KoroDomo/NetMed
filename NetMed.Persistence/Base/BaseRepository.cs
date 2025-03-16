using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;
using NetMed.Infraestructure.Validators;
using NetMed.Infraestructure.Logger;

namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly NetMedContext _context;
        protected readonly ICustomLogger _logger;
        protected readonly OperationValidator _operations;
        protected DbSet<TEntity> Entity { get; }

        public BaseRepository(NetMedContext context, ICustomLogger logger, MessageMapper messageMapper)
        {
            _context = context;
            _logger = logger;
            Entity = _context.Set<TEntity>();
            _operations = new OperationValidator(messageMapper);
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var datos = await Entity.Where(filter).ToListAsync();
                return _operations.SuccessResult(datos, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                return _operations.HandleException("Operations", "GetFailed");
            }
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var datos = await Entity.ToListAsync();
                return _operations.SuccessResult(datos, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                return _operations.HandleException("Operations", "GetFailed");
            }
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            try
            {
                var entity = await Entity.FindAsync(id);
                if (entity == null)
                {
                    return _operations.HandleException("Operations", "GetFailed");
                }

                return _operations.SuccessResult(entity, "Operations", "GetFailed");
            }
            catch (Exception ex)
            {
                return _operations.HandleException("Operations", "GetFailed");
            }
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            try
            {

                if (entity == null)
                {
                    return _operations.HandleException("Operations", "SaveFailed");
                }

                Entity.Add(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, "Operations", "SaveSuccess");
            }
            catch (Exception ex)
            {
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.HandleException("Operations", "UpdateFailed");
                }

                Entity.Update(entity); 
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, "Operations", "UpdateSuccess");
            }
            catch (Exception ex)
            {
                return _operations.HandleException("Operations", "UpdateFailed");
            }
        }

        
    }
}