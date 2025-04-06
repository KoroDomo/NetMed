using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.NetworkType;

namespace NetMed.ApiConsummer.Persistence.Interfaces
{
    public interface INetworkTypeRepository
    {
        Task<ListOperationResult<GetNetworkTypeModel>> GetAllNetworksAsync();
        Task<OperationResult<TEntity>> GetNetworkByIdAsync<TEntity>(int id);
        Task<OperationResult<SaveNetworkTypeModel>> CreateNetworkAsync(SaveNetworkTypeModel model);
        Task<OperationResult<UpdateNetworkTypeModel>> UpdateNetworkAsync(UpdateNetworkTypeModel model);
        Task<OperationResult<RemoveNetworkTypeModel>> DeleteNetworkAsync(RemoveNetworkTypeModel model);
    }
}
