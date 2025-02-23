
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly NetmedContext _context;
        private readonly ILogger<BaseRepository<TEntity>> _logger;
        private readonly IConfiguration _configuration;

        private DbSet<TEntity> Entity { get; set; }

        protected BaseRepository(NetmedContext context, ILogger<BaseRepository<TEntity>> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            Entity = _context.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return await Entity.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                throw; 
            }
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await Entity.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                throw; 
            }
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await Entity.Where(filter).ToListAsync();
                result.Data = datos;
                result.Success = true;
                result.Mesagge = _configuration["ErrorMessages:EntityRetrieved"];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                result.Success = false;
                result.Mesagge = _configuration["ErrorMessages:GeneralError"];
            }

            return result;
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:EntityNotFound"], "id", id);
                    throw new KeyNotFoundException(_configuration["ErrorMessages:EntityNotFound"]);
                }

                _logger.LogInformation(_configuration["ErrorMessages:EntityRetrieved"], "id", id);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                throw; 
            }
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                

                result.Mesagge = _configuration["SuccessMesagge:EntityCreated"];
                _logger.LogInformation(_configuration["ErrorMessages:EntityCreated"], "entity", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                result.Success = false;
                result.Mesagge = _configuration["ErrorMessages:GeneralError"];
            }

            return result;
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Mesagge = _configuration["ErrorMessages:EntityUpdated"];
                _logger.LogInformation(_configuration["ErrorMessages:EntityUpdated"], "entity", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                result.Success = false;
                result.Mesagge = _configuration["ErrorMessages:GeneralError"];
            }

            return result;
        }

        public virtual async Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Remove(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Mesagge = _configuration["ErrorMessages:EntityDeleted"];
                _logger.LogInformation(_configuration["ErrorMessages:EntityDeleted"], "entity", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                result.Success = false;
                result.Mesagge = _configuration["ErrorMessages:GeneralError"];
            }

            return result;
        }
    }
}
