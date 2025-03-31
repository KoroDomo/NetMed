using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly NetMedContext _context;

        protected DbSet<TEntity> Entity { get;}  

        public BaseRepository(NetMedContext context) 
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }
        public virtual async Task<OperationResult> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
               await Entity.AnyAsync(filter);
            }
            catch (Exception)
            {
                result.success = false;
                result.message = "Ocurrio un error al obtener los datos";
            }
            return result;
        }
        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
               var datos = await Entity.Where(filter).ToListAsync();
            }
            catch (Exception)
            {
                result.success = false;
                result.message = "Ocurrio un error al obtener los datos";
            }
            return result;
            
        }
        public virtual async Task<List<TEntity>> GetAllAsync()
        {

            OperationResult result = new OperationResult();
            try
            {
                var datos = await Entity.ToListAsync();
            }
            catch (Exception)
            {
                result.success = false;
                result.message = "Ocurrio un error al obtener los datos";
            }
            return (List<TEntity>)result.data;
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                await Entity.FindAsync(Id);
            }
            catch (Exception)
            {
                result.success = false;
                result.message = "Ocurrio un error al obtener los datos por ID";
            }
            return result.data;
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
               Entity.Add(entity); 
               await _context.SaveChangesAsync();
            }
            catch (Exception) 
            {
                result.success = false;
                result.message = "Ocurrio un error al guardar los datos";
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
            }
            catch (Exception)
            {
                result.success = false;
                result.message = "Ocurrio un error al actualizar los datos";
            }
            return result;    
        }

        public virtual async Task<OperationResult> RemoveAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(Id);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    await _context.SaveChangesAsync();
                    result.success = true;
                    result.message = "Datos Elminados con exito";   
                }
                else
                {
                    result.success = false;
                    result.message = "El registro no fue encontrado.";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = $"Ocurrió un error al eliminar los datos: {ex.Message}";
            }
            return result;
        }
    }
}
