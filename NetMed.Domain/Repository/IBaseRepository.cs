using NetMed.Domain.Base;
using System.Linq.Expressions;

namespace NetMed.Domain.Repository
{
    /// <summary>
    /// Interfaz que deben heredar todos los repositorios
    /// </summary>
    /// <typeparam name="TEntity">Entidad</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetEntityByIdAsync(int id);
        Task<OperationResult> UpdateEntityAsync(TEntity entity);  
        Task<OperationResult> SaveEntityAsync(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync();

        Task<OperationResult> DeleteEntityAsync(TEntity entity);




    }
}
