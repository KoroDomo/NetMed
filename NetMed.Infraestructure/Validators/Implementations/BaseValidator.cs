using NetMed.Domain.Base;
using NetMed.Infraestructure.Validators.Interfaces;

namespace NetMed.Infraestructure.Validators.Implementations
{
    public class BaseValidator : IBaseValidator
    {
        private readonly JsonMessage _messageMapper;

        public BaseValidator()
        {
            _messageMapper = new JsonMessage();
        }

        public OperationResult SuccessResult(dynamic result, string className, string methodName)
        {
            return new OperationResult
            {
                Success = true,
                Message = GetSuccesMessage(className, methodName),
                Result = result
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
            return _messageMapper.ErrorMessages[className][methodName];
        }

        public string GetSuccesMessage(string className, string methodName)
        {
            return _messageMapper.SuccessMessages[className][methodName];

        }


    }
}
