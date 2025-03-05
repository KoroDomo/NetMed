using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Persistence.Validators
{
    public class InsuranceProviderValidator : OperationValidator
    {
        private OperationValidator _operationValidator;
        public InsuranceProviderValidator(IConfiguration configuration) : base(configuration)
        {
            _operationValidator = new OperationValidator(configuration);
        }

        public OperationResult validateInsuranceProvider(InsuranceProviders insuranceProvider)
        {
            OperationResult result = new OperationResult() {Success=true };

            while (result.Success != false)
            {
                result = _operationValidator.isNull(insuranceProvider);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Name, 100);
                result = _operationValidator.ValidateStringLength(insuranceProvider.PhoneNumber, 15);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Email, 100);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Website, 255);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Address, 255);
                result = _operationValidator.ValidateStringLength(insuranceProvider.City, 100);
                result = _operationValidator.ValidateStringLength(insuranceProvider.State, 100);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Country, 100);
                result = _operationValidator.ValidateStringLength(insuranceProvider.ZipCode, 10);
                result = _operationValidator.ValidateStringLength(insuranceProvider.CoverageDetails, 20000);
                result = _operationValidator.ValidateStringLength(insuranceProvider.LogoUrl, 255);
                result = _operationValidator.IsInt(insuranceProvider, insuranceProvider.NetworkTypeID);
                result = _operationValidator.ValidateStringLength(insuranceProvider.CustomerSupportContact, 15);
                result = _operationValidator.ValidateStringLength(insuranceProvider.AcceptedRegions, 255);
                result = _operationValidator.ValidateStringLength(insuranceProvider.MaxCoverageAmount.ToString(), 12);
                result = _operationValidator.ValidateStringLength(insuranceProvider.Address, 255);
                
                break;
            } 
            return result;
        }
    }
}
