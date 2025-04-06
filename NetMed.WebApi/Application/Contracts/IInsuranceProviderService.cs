using NetMed.ApiConsummer.Application.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;

namespace NetMed.ApiConsummer.Application.Contracts
{
    public interface IInsuranceProviderService : IBaseService<GetInsuranceProviderModel,SaveInsuranceProviderModel, UpdateInsuranceProviderModel, RemoveInsuranceProviderModel>
    {
        
    }
}
