using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;

namespace NetMed.Infraestructure.Validators.Interfaces
{
    public interface IInsuranceProviderValidator : IOperationValidator
    {
        public OperationResult ValidateInsuranceProvider(InsuranceProviders insuranceProvider);
        public OperationResult ValidateNameExists(InsuranceProviders insuranceProvider, NetMedContext context);
    }
}
