using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly NetMedContext _context;
        protected readonly ICustomLogger _logger;
        protected readonly OperationValidator _operations;
        protected DbSet<TEntity> Entity { get; }

        public BaseRepository(NetMedContext context, ICustomLogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            Entity = _context.Set<TEntity>();
            _operations = new OperationValidator(configuration);
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var datos = await Entity.Where(filter).ToListAsync();
                return _operations.SuccessResult(datos, "BaseRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetAllAsync");
            }
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var datos = await Entity.ToListAsync();
                return _operations.SuccessResult(datos, "BaseRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetAllAsync");
            }
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            try
            {
                var entity = await Entity.FindAsync(id);
                if (entity == null)
                {
                    return _operations.SuccessResult(null, "BaseRepository.GetEntityByIdAsync");
                }

                return _operations.SuccessResult(entity, "BaseRepository.GetEntityByIdAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetEntityByIdAsync");
            }
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            try
            {

                if (entity == null)
                {
                    return _operations.SuccessResult(null, "La entidad no puede ser nula.");
                }

                Entity.Add(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, "BaseRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.SaveEntityAsync");
            }
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null, "La entidad no puede ser nula.");
                }

                Entity.Update(entity); 
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, "BaseRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.UpdateEntityAsync" );
            }
        }

        
    }
}