using NetMed.Application.Base;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Base;

namespace NetMed.Application.Contracts
{
    public interface INetworkTypeService:IBaseService<SaveNetworkTypeDto, UpdateNetworkTypeDto, RemoveNetworkTypeDto>
    {
        Task<OperationResult> GetNetworkTypeAsync();

    }
}
