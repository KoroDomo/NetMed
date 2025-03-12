using NetMed.Domain.Base;

namespace NetMed.Persistence.Validators
{
    public class BaseValidator
    {
        private readonly MessageMapper _messageMapper;

        public BaseValidator(MessageMapper messageMapper)
        {
            _messageMapper = messageMapper;
        }

        public OperationResult SuccessResult(dynamic result, string className, string methodName )
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
