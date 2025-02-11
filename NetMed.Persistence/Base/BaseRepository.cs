
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           

namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
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
               //var datos = Entity.Where(filter).ToList();
               //result.Result = datos;
                result.Result = await Entity.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(TType id)
        {
            return await Entity.FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
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
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                   result.Success = false;
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
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";

            }

            return result;
        }

        public virtual async Task<OperationResult> GetAllAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.Result = await Entity.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }
        Task<List<OperationResult>> IBaseRepository<TEntity, TType>.GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                result.Result = Entity.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return Task.FromResult(new List<OperationResult> { result });
        }

        //    Task<OperationResult> IBaseRepository<TEntity, TType>.GetAll(Expression<Func<TEntity, bool>> filter)
        //    { 
        //        throw new NotImplementedException();
        //    }

        //    Task<bool> IBaseRepository<TEntity, TType>.Exists(Expression<Func<TEntity, bool>> filter)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //internal interface IBaseRepository<TEntity> where TEntity : class
        //{
        //}
    }
}
