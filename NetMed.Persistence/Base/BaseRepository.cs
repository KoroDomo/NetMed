using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
        private readonly NetMedContext _context;
        private readonly ILogger<BaseRepository<TEntity, TType>> _logger;
        private readonly IConfiguration _configuration;
        private readonly Operations _operations;

        private DbSet<TEntity> _entity { get; set; }

        public BaseRepository(NetMedContext context,
                             ILogger<BaseRepository<TEntity, TType>> logger,
                             IConfiguration configuration,
                             Operations operations)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = operations;
            _entity = _context.Set<TEntity>();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await _entity.AnyAsync(filter);
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
                var datos = await _entity.Where(filter).ToListAsync();
                return _operations.SuccessResult(datos, _configuration, "BaseRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetAllAsync", _configuration);
            }
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var datos = await _entity.ToListAsync();
                return _operations.SuccessResult(datos, _configuration, "BaseRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetAllAsync", _configuration);
            }
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(TType id)
        {
            try
            {
                var entity = await _entity.FindAsync(id);
                if (entity == null)
                {
                    return _operations.SuccessResult(null, _configuration, "BaseRepository.GetEntityByIdAsync");
                }

                return _operations.SuccessResult(entity, _configuration, "BaseRepository.GetEntityByIdAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.GetEntityByIdAsync", _configuration);
            }
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null, _configuration, "La entidad no puede ser nula.");
                }

                _entity.Add(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, _configuration, "BaseRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.SaveEntityAsync", _configuration);
            }
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null, _configuration, "La entidad no puede ser nula.");
                }

                _entity.Update(entity); 
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, _configuration, "BaseRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "BaseRepository.UpdateEntityAsync", _configuration);
            }
        }
    }
}