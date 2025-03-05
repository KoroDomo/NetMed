using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface INetworkTypeRepository : IBaseRepository<NetworkType>
    {
        Task<OperationResult> RemoveNetworkTypeAsync(int id);
        Task<OperationResult> GetNetworkTypeById(int networkTypeId);
    }
}
