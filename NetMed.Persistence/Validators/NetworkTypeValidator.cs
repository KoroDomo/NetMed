
using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Persistence.Validators
{
    public class NetworkTypeValidator : OperationValidator
    {
        private OperationValidator _operationValidator;
        public NetworkTypeValidator(IConfiguration configuration) : base(configuration)
        {
            _operationValidator = new OperationValidator(configuration);
        }

        public OperationResult validateNetworkType(NetworkType networkType)
        {
            OperationResult result= new OperationResult() {Success=true };

            while(result.Success != false)
            {
                result = _operationValidator.isNull(networkType);
                result = _operationValidator.ValidateStringLength(networkType.Name, 50);
                result = _operationValidator.ValidateStringLength(networkType.Description, 255);
                
                break;
            }
            return result;
        }
    }
}
