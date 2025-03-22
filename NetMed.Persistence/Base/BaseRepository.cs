using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
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
                result.Success = false;
                result.Message = "Ocurrio un error al obtener los datos";
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
                result.Success = false;
                result.Message = "Ocurrio un error al obtener los datos";
            }
            return result;
            
        }
        public virtual async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                var datos = await Entity.ToListAsync();
                return result;
            } 
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al obtener los datos";
            }
            return result;
           
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                await Entity.FindAsync(Id);
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al obtener los datos por ID";
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
            catch (Exception) 
            {
                result.Success = false;
                result.Message = "Ocurrio un error al guardar los datos";
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
                result.Success = false;
                result.Message = "Ocurrio un error al actualizar los datos";
            }
            return result;    
        }

        public virtual async Task<OperationResult> RemoveAsync(int Id)
        {
            OperationResult result= new OperationResult();
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(Id);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error al desactivar los datos";
            }
            return result;
        }
    }
}
