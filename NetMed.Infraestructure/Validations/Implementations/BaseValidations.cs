

using NetMed.Domain.Base;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Infrastructure.Validations.Interfaces;

namespace NetMed.Infrastructure.Validations.Implementations
{
    public class BaseValidations : IBaseValidations
    {
        private readonly RepErrorMapper _messageMapper;

        public BaseValidations()
        {
            _messageMapper = new RepErrorMapper();
        }

        public OperationResult SuccessResult(dynamic result, string className, string methodName)
        {
            return new OperationResult
            {
                Success = true,
                Message = GetSuccesMessage(className, methodName),
                data = result
            };
        }

        public OperationResult HandleException(string className, string methodName)
        {
            return new OperationResult
            {
                Success = false,
                Message = GetErrorMessage(className, methodName)
            };
        }

        public string GetErrorMessage(string className, string methodName)
        {
            var errorMessages = _messageMapper.GetType().GetProperty($"Error{className}RepositoryMessages")?.GetValue(_messageMapper) as Dictionary<string, string>;
            return errorMessages != null && errorMessages.ContainsKey(methodName) ? errorMessages[methodName] : string.Empty;
        }

        public string GetSuccesMessage(string className, string methodName)
        {
            var successMessages = _messageMapper.GetType().GetProperty($"Success{className}RepositoryMessages")?.GetValue(_messageMapper) as Dictionary<string, string>;
            return successMessages != null && successMessages.ContainsKey(methodName) ? successMessages[methodName] : string.Empty;
        }
    }
}

