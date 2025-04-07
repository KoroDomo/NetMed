using NetMed.Domain.Base;
using NetMedWebApi.Models;

namespace NetMedWebApi.Infrastructure.ApiClient.Interfaces
{
    public interface IClientApi
    {
        Task<OperationResult<T>> GetByIdAsync<T>(string uri, int id);
        Task<OperationResultList<T>> GetAllAsync<T>(string uri);

        Task<OperationResult<T>> PostAsync<T>(string uri, T model);

        Task<OperationResult<T>> PutAsync<T>(string uri, T model);

        Task<OperationResult<T>> DeleteAsync<T>(string uri, T model);


    }
}
