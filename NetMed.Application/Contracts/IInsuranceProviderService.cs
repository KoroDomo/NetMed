using NetMed.Application.Base;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Base;

namespace NetMed.Application.Contracts
{
    public interface IInsuranceProviderService:IBaseService<SaveInsuranceProviderDto, UpdateInsuranceProviderDto, RemoveInsuranceProviderDto>
    {                                                   
        //Task<OperationResult> GetInsuranceProvidersAsync();

    }
}
