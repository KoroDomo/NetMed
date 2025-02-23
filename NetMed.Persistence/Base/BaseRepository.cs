using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using System.Linq.Expressions;
using NetMed.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly NetMedContext _context;
        private DbSet<TEntity> Entity { get; set; } 
        public BaseRepository(NetMedContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();  
        }
        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error guardando los datos";
            }
            return result;
        }
        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult() { Success = true };

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error actualizando los datos";
            }
            return result;
        }
        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await Entity.Where(filter).ToListAsync();
                result.Success = true;
                result.Data = datos;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo los datos";

            }
            return result;
        }
        public virtual async Task<TEntity> GetEntityByIdAsync(short id)
        {
            return await Entity.FindAsync(id);
        }
        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            //return await _context.Set<TEntity>().CountAsync(filter) > 0;
            return await Entity.AnyAsync(filter);
        }
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }
    }
}
