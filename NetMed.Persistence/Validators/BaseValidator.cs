
using Microsoft.Extensions.Configuration;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Validators
{
    public class BaseValidator
    {
        private readonly IConfiguration _configuration;

        public BaseValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OperationResult SuccessResult(dynamic result, string configKey = null)
        {
            string successMessage = _configuration[$"Messages:Success:{configKey}"];

            return new OperationResult
            {
                Success = true,
                Result = result,
                Message = successMessage
            };
        }

        public OperationResult HandleException(Exception ex, string configKey)
        {
            string errorMessage = _configuration[$"Messages:Error:{configKey}"];

            return new OperationResult
            {
                Result = null,
                Success = false,
                Message = $"{errorMessage}"
            };
        }
    }
}
