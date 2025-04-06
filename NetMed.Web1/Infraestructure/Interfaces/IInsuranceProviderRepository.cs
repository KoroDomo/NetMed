using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;

namespace NetMed.ApiConsummer.Persistence.Interfaces
{
    public interface IInsuranceProviderRepository
    {
        Task<ListOperationResult<GetInsuranceProviderModel>> GetAllProvidersAsync();
        Task<OperationResult<TEntity>> GetProviderByIdAsync<TEntity>(int id);
        Task<OperationResult<SaveInsuranceProviderModel>> CreateProviderAsync(SaveInsuranceProviderModel model);
        Task<OperationResult<UpdateInsuranceProviderModel>> UpdateProviderAsync(UpdateInsuranceProviderModel model);
        Task<OperationResult<RemoveInsuranceProviderModel>> DeleteProviderAsync(RemoveInsuranceProviderModel model);

    }
}
