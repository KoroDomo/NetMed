using NetMed.Domain.Base;
using System.Linq.Expressions;

namespace NetMed.Domain.Repository
{
    /// <summary>
    /// Interfaz que deben heredar todos los repositorios
    /// </summary>
    /// <typeparam name="TEntity">Entidad</typeparam>
    /// <typeparam name="TType">El tipo de dato del primary key para realizar consulta</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> GetEntityByIdAsync(int Id);
        Task<OperationResult> UpdateEntityAsync(TEntity entity);  
        Task<OperationResult> SaveEntityAsync(TEntity entity);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> ExistsAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> RemoveAsync(int Id);
    }
}
