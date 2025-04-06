
namespace WebApiApplication.Persistence.Base
{
    public interface IBaseRepository<TModel, TSave, TUpdate, TRemove>
    {
        Task<List<TModel>> GetAllAsync(string endpoint);
        Task<TModel> GetByIdAsync(string endpoint);
        Task<bool>SaveAsync(string endpoint, TSave model);
        Task<bool> UpdateAsync(string endpoint, TUpdate model);
        Task<bool> RemoveAsync(string endpoint, TRemove model);
    }
}
