using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Context;
using System.Timers;

namespace NetMed.Infraestructure.Validators.Implementations
{
    public class InsuranceProviderValidator : OperationValidator, IInsuranceProviderValidator
    {
        private readonly OperationValidator _operationValidator;

        public InsuranceProviderValidator()
        {
            _operationValidator = new OperationValidator();
        }

        public OperationResult ValidateInsuranceProvider(InsuranceProviders insuranceProvider)
        {
            try
            {


                var result = _operationValidator.isNull(insuranceProvider);
                if (result.Success == false)
                {
                    result.Message = _operationValidator.GetErrorMessage("Insurances", "isNull");
                    return result;
                }

                result = IsValidEmail(insuranceProvider.Email);
                if (result.Success == false)
                {
                    return result;
                }

                result = ValidateNumericProperties(insuranceProvider);
                if (result.Success == false)
                {
                    return result;
                }

                result = IsValidPhoneNumber(insuranceProvider.ContactNumber);
                if (result.Success == false)
                {
                    return result;
                }

                result = IsValidPhoneNumber(insuranceProvider.CustomerSupportContact);
                if (result.Success == false)
                {
                    return result;
                }
                result = ValidateStringProperties(insuranceProvider);
                if (result.Success==false)
                {
                    return result;
                }



                return new OperationResult { Success = true, Result = insuranceProvider, Message = result.Message };
            }
            catch (Exception ex)
            {
                return new OperationResult { Message = ex.Message };
            }
        }

        private OperationResult ValidateStringProperties(InsuranceProviders insuranceProvider)
        {
            var validations = new (string Value, int MaxLength, string FieldName)[]
            {
                (insuranceProvider.Name, 100, nameof(insuranceProvider.Name)),
                (insuranceProvider.Email, 100, nameof(insuranceProvider.Email)),
                (insuranceProvider.Website, 255, nameof(insuranceProvider.Website)),
                (insuranceProvider.Address, 255, nameof(insuranceProvider.Address)),
                (insuranceProvider.City, 100, nameof(insuranceProvider.City)),
                (insuranceProvider.State, 100, nameof(insuranceProvider.State)),
                (insuranceProvider.Country, 100, nameof(insuranceProvider.Country)),
                (insuranceProvider.ZipCode, 10, nameof(insuranceProvider.ZipCode)),
                (insuranceProvider.AcceptedRegions, 255, nameof(insuranceProvider.AcceptedRegions))
            };

            foreach (var validation in validations)
            {
                var result = _operationValidator.ValidateStringLength(validation.Value, validation.MaxLength);
                if (result.Success == false)
                {
                    result.Message = $"El campo {validation.FieldName} excede la longitud maxima permitida.";
                    result.Success = false;

                    return result;
                }
            }

            return new OperationResult { Success = true, Result = insuranceProvider };
        }

        private OperationResult ValidateNumericProperties(InsuranceProviders insuranceProvider)
        {
            var result = _operationValidator.IsInt(insuranceProvider, insuranceProvider.NetworkTypeID);
            if (result.Success == false)
            {
                result.Message = _operationValidator.GetErrorMessage("Insurances", "NetworkTypeID");
                result.Success = false;
                return result;
            }

            if (insuranceProvider.MaxCoverageAmount <= 0)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = _operationValidator.GetErrorMessage("Insurances", "MaxCoverageAmount")
                };
            }

            return new OperationResult { Success = true, Result = insuranceProvider };
        }

        public OperationResult ValidateNameExists(InsuranceProviders insuranceProvider, NetMedContext context)
        {
            if (context.InsuranceProviders.Any(ip => ip.Name == insuranceProvider.Name && ip.Id != insuranceProvider.Id))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = _operationValidator.GetErrorMessage("Insurances", "Name")
                };
            }

            return new OperationResult { Success = true, Result = insuranceProvider };
        }
    }
}