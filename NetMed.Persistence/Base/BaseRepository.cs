
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           

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
                //var datos = Entity.Where(filter).ToList();
                //result.Result = datos;
                result.data = await Entity.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public virtual async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.FindAsync(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
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
                result.Message = "Datos guardados correctamente";
                result.Success = true;
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
                result.data = entity;
                result.Success = true;
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
                result.data = await Entity.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }
        public virtual async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await Entity.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + "Error al llamar los datos";
                result.Success = false;
            }

            return result;
        }

        public virtual async Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult() { Success = true };
            try
            {
                Entity.Remove(entity);
                await _context.SaveChangesAsync();
                result.data = entity;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error borrando los datos.";

            }

            return result;
        }

      
    }
}
