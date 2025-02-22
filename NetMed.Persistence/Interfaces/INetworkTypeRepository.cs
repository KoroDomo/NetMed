using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface INetworkTypeRepository : IBaseRepository<NetworkTypes>
    {
        Task<OperationResult> DeleteNetworkTypeAsync(int id);
        Task<OperationResult> GetNetworkTypesByProviderAsync(int providerId);
    }
}
