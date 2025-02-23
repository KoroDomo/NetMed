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
        Task<TEntity> GetEntityByIdAsync(short id);
        Task<OperationResult> UpdateEntityAsync(TEntity entity);  
        //Task DeleteEntityAsync(TEntity entity);
        Task<OperationResult> SaveEntityAsync(TEntity entity);    
        Task<List<TEntity>> GetAllAsync();
        Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);

    }
}
