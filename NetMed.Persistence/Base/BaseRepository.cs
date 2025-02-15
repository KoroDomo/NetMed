using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

//Hay que implemantar el DRY aqui
namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
        private readonly NetMedContext _context;
        private readonly ILogger<BaseRepository<TEntity, TType>> _logger;


        private DbSet<TEntity> _entity { get; set; }
        public BaseRepository(NetMedContext context, ILogger<BaseRepository<TEntity, TType>> logger) 
        {
            _context = context;
            _logger = logger;
            _entity = _context.Set<TEntity>();
        }
        
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var entity = await _entity.AnyAsync(filter);
                operationR.Result = entity;
                
            }
            catch (Exception ex)
            {

                operationR.Success = false;
                operationR.Result = $"Ocurrió un error comprobando la entidad.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }

            return operationR.Success;
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                
                var datos = await _entity.Where(filter).ToListAsync();
                operationR.Result = datos;

            }
            catch (Exception ex)
            {
                operationR.Success = false;
                operationR.Mesagge = "Ocurrio un error obteniendo los datos.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }

            return operationR;
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            OperationResult operationR = new OperationResult();

            try
            {
                var datos = await this._entity.ToListAsync();
                operationR.Result = datos;
            }
            catch (Exception ex)
            {

                operationR.Success = false;
                operationR.Mesagge = $"Ocurrió un error obteniendo los datos.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }

            return operationR;
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(TType id)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var entity = await this._entity.FindAsync(id);
                operationR.Result = entity;
            }
            catch (Exception ex)
            {

                operationR.Success = false;
                operationR.Result = $"Ocurrió un error obteniendo la entidad.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }
            return operationR;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                _entity.Add(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                operationR.Success = false;
                operationR.Mesagge = "Ocurrio un error guardando los datos.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }

            return operationR;
            
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                _entity.Add(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                operationR.Success = false;
                operationR.Mesagge = "Ocurrio un error actualizando los datos.";
                _logger.LogError(operationR.Mesagge, ex.ToString());
            }

            return operationR;
        }

        
    }

}
