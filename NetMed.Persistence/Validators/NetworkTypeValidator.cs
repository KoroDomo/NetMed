
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;

namespace NetMed.Persistence.Validators
{
    public class NetworkTypeValidator : OperationValidator
    {
        private OperationValidator _operationValidator;
        public NetworkTypeValidator(IConfiguration configuration) : base(configuration)
        {
            _operationValidator = new OperationValidator(configuration);
        }

        public OperationResult ValidateNetworkType(NetworkType networkType)
        {
            var result = _operationValidator.isNull(networkType);
            if (!result.Success)
            {
                result.Message = "El networkType es nulo.";
                return result;
            }

            result = ValidateStringProperties(networkType);
            if (!result.Success)
                return result;

            return new OperationResult { Success = true, Result = networkType };
        }

        private OperationResult ValidateStringProperties(NetworkType networkType)
        {
            var validations = new (string Value, int MaxLength, string FieldName)[]
            {
                (networkType.Name, 50, nameof(networkType.Name)),
                (networkType.Description, 2000, nameof(networkType.Description))
            };

            foreach (var validation in validations)
            {
                var result = _operationValidator.ValidateStringLength(validation.Value, validation.MaxLength);
                if (!result.Success)
                {
                    result.Message = $"El campo {validation.FieldName} excede la longitud máxima permitida.";
                    return result;
                }
            }

            return new OperationResult { Success = true, Result = networkType };
        }

        public OperationResult ValidateNetworkTypeNameExists(NetworkType networkType, NetMedContext context)
        {
            if (context.InsuranceProviders.Any(ip => ip.Name == networkType.Name && ip.Id != networkType.Id))
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Ya existe un NetworkType con este nombre."
                };
            }

            return new OperationResult { Success = true, Result = networkType };
        }

    }
}
