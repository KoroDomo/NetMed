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
        private DbSet<TEntity> Entity { get; set; }  

        protected BaseRepository(NetMedContext context) 
        {
            _context = context;
            Entity = _context.Set<TEntity>();
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
            }
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Ocurrio un error al obtener los datos";
            }
            return result;
            
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
           return await Entity.FindAsync(id);
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
                result.success = false;
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
            catch (Exception ex)
            {
                result.success = false;
                result.Message = "Ocurrio un error al actualizar los datos";
            }
            return result;
           
        }
    }
}
