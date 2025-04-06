using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;

namespace NetMed.ApiConsummer.Persistence.Interfaces
{
    public interface IInsuranceProviderRepository
    {
        Task<OperationResult> GetAllProvidersAsync();
        Task<OperationResult> GetProviderByIdAsync<TEntity>(int id);
        Task<OperationResult> CreateProviderAsync(SaveInsuranceProviderModel model);
        Task<OperationResult> UpdateProviderAsync(UpdateInsuranceProviderModel model);
        Task<OperationResult> DeleteProviderAsync(RemoveInsuranceProviderModel model);

    }
}
