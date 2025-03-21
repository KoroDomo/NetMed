
using Microsoft.EntityFrameworkCore;
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
        private readonly JsonMessage _messageMapper;

        private DbSet<TEntity> Entity { get; set; }

        protected BaseRepository(NetmedContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

       
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            { 
                return await Entity.AnyAsync(filter);
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
                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);
                throw; 
            }
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await Entity.Where(filter).ToListAsync();
            
                _logger.LogInformation(_messageMapper.SuccessMessages["GetAllEntity"]);
                return new OperationResult { Success = true, Message = _messageMapper.SuccessMessages["GetAllEntity"], Data = datos };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = _messageMapper.ErrorMessages["GeneralError"], Data = result };
            }

        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning(_messageMapper.ErrorMessages["EntityNotFound"], "id", id);
                    throw new KeyNotFoundException(_messageMapper.ErrorMessages["EntityNotFound"]);
                }

                _logger.LogInformation(_messageMapper.SuccessMessages["EntityRetrieved"], "id", id);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);

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

               _logger.LogInformation(_messageMapper.SuccessMessages["EntityCreated"]);
                return new OperationResult { Success = true, Message = _messageMapper.SuccessMessages["EntityCreated"], Data = entity };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = _messageMapper.ErrorMessages["GeneralError"], Data = result };
            }


        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();

            
                _logger.LogInformation(_messageMapper.SuccessMessages["EntityUpdated"]);
                return new OperationResult { Success = true, Message = _messageMapper.SuccessMessages["EntityUpdated"], Data = entity };
            }
            catch (Exception ex)
            {


                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = _messageMapper.ErrorMessages["GeneralError"], Data = result };
            }

        }

        public virtual async Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Remove(entity);
                await _context.SaveChangesAsync();


               return new OperationResult { Success = true, Message = _messageMapper.SuccessMessages["EntityDeleted"], Data = entity };
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, _messageMapper.ErrorMessages["DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Message = _messageMapper.ErrorMessages["GeneralError"], Data = result };
            }

        }
    }
}
