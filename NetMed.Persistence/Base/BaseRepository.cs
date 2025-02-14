using NetMed.Domain.Repository;
using NetMed.Domain.Base;
using System.Linq.Expressions;
using NetMed.Persistence.Context;
using Microsoft.EntityFrameworkCore;

//Hay que implemantar el DRY aqui
namespace NetMed.Persistence.Base
{
    public class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
        private readonly NetMedContext _context;
        private DbSet<TEntity> Entity { get; set; }
        public BaseRepository(NetMedContext context) 
        {
            _context = context;
            Entity= _context.Set<TEntity>();
        }
        
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                
                var datos = Entity.Where(filter).ToListAsync();
                result.Result = datos;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Ocurrio un error obteniendo los datos.";
            }

            return result;
        }

        public virtual async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.Entity.ToListAsync();
                result.Result = datos;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Mesagge = $"Ocurrió un error obteniendo los datos.";
            }

            return result;
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(TType id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await this.Entity.FindAsync(id);
                result.Result = entity;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Result = $"Ocurrió un error obteniendo la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Ocurrio un error guardando los datos.";
            }

            return result;
            
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Ocurrio un error actualizando los datos.";
            }

            return result;
        }

        Task<OperationResult> IBaseRepository<TEntity, TType>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}
