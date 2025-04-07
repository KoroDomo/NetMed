using NetMedWebApi.Models;

namespace NetMedWebApi.Persistence.Interfaces
{
    public interface IRepository<TModel, saveMode, updateModel, deleteModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetEntityByIdAsync(int id);
        Task<OperationResult<updateModel>> UpdateEntityAsync(updateModel entity);
        Task<OperationResult<saveMode>> SaveEntityAsync(saveMode entity);
        Task<OperationResult<deleteModel>> DeleteEntityAsync(deleteModel entity);

    }
}
