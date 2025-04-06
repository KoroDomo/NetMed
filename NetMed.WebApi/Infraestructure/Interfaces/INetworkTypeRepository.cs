using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.NetworkType;

namespace NetMed.ApiConsummer.Persistence.Interfaces
{
    public interface INetworkTypeRepository
    {
        Task<List<GetNetworkTypeModel>> GetAllNetworksAsync();
        Task<TEntity> GetNetworkByIdAsync<TEntity>(int id);
        Task CreateNetworkAsync(SaveNetworkTypeModel model);
        Task UpdateNetworkAsync(UpdateNetworkTypeModel model);
        Task DeleteNetworkAsync(RemoveNetworkTypeModel model);
    }
}
