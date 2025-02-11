
using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Repository;
using NetMed.Persistence.Context;
using System.Linq.Expressions;

namespace NetMed.Persistence.Base
{
    public abstract class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType> where TEntity : class
    {
       private readonly NetmedContext _context;



        private DbSet<TEntity> Entity{ get; set; }

        protected BaseRepository(NetmedContext context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();

        }


        

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
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
                result.success = false;
                result.Mesagge = "Ocurrio un error obteniendo los datos.";
            }
            return result;

        }

        public virtual async Task<TEntity> GetEntityByIdAsync(TType id)
        {
            return await Entity.FindAsync(id);

        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult() ;

            try
            {
                Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
            } 
            catch (Exception ex) 
            {
                result.success = false ;
                result.Mesagge = "Ocurrio un error guardando los datos.";
            }
            return result ;
           
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult() ;

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.success = false;
                result.Mesagge = "Ocurrio un error actualizando los datos.";
            }
            return result;
           


        }

    }    
}
