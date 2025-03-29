
using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Infraestructure.Validators.Interfaces
{
    public interface IInsuranceProviderServiceValidator : IInsuranceProviderValidator
    {
        public Task<OperationResult> ValidateNetworkTypeID(InsuranceProviders insuranceProviders);
    }
}
