using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity, TType> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly NetMedContext _context;

        private readonly OperationValidator _operations;

        private readonly ILogger<BaseRepository<TEntity, TType>> _logger;
        private readonly IConfiguration _configuration;

        private DbSet<TEntity> Entity { get; set; }

        public BaseRepository(NetMedContext context,
                             ILogger<BaseRepository<TEntity, TType>> logger,
                             IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            Entity = _context.Set<TEntity>();
            _operations = new OperationValidator(_configuration);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await Entity.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al verificar la existencia de la entidad con el filtro: {filter.ToString()}");
                throw; 
            }
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