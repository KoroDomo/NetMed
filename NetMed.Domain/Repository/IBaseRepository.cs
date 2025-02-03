using NetMed.Domain.Base;
using System.Linq.Expressions;

namespace NetMed.Domain.Repository
{
    /// <summary>
    /// Interfaz que deben heredar todos los repositorios
    /// </summary>
    /// <typeparam name="TEntity">Entidad</typeparam>
    /// <typeparam name="TType">El tipo de dato del primary key para realizar consulta</typeparam>
    public interface IBaseRepository<TEntity, TType> where TEntity : class
    {
        Task<TEntity> GetEntityByIdAsync(TType id);
        Task UpdateEntityAsync(TEntity entity);  
        Task DeleteEntityAsync(TEntity entity); 
        Task SaveEntityAsync(TEntity entity);    
        Task<List<TEntity>> GetAllAsync();
        Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);

    }
}
