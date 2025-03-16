using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Context;

namespace NetMed.Infraestructure.Validators.Implementations
{
    public class NetworkTypeValidator : OperationValidator, INetworkTypeValidator
    {
        private OperationValidator _operationValidator;
        public NetworkTypeValidator() 
        {
            _operationValidator = new OperationValidator();
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

        public OperationResult ValidateStringProperties(NetworkType networkType)
        {
            var validations = new (string Value, int MaxLength, string FieldName)[]
            {
                (networkType.Name, 50, nameof(networkType.Name))
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

        public OperationResult ValidateNameExists(NetworkType networkType, NetMedContext context)
        {
            if (context.NetworkType.Any(n => n.Name == networkType.Name && n.Id != networkType.Id))
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
