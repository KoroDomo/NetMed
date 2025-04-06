using NetMed.ApiConsummer.Core.Base;

namespace NetMed.ApiConsummer.Core.Repository
{
    public interface IBaseRepository : IResponseHttps
    {
        Task<OperationResult<TResult>> GetAsync<TResult>(string endpoint, int id);
        Task<ListOperationResult<TResult>> GetAllAsync<TResult>(string endpoint);
        Task<OperationResult<TResult>> CreateAsync<TResult>(string endpoint, TResult model);
        Task<OperationResult<TResult>> UpdateAsync<TResult>(string endpoint, TResult model);
        Task<OperationResult<TResult>> DeleteAsync<TResult>(string endpoint, TResult model);

    }
}
