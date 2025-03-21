using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using System.Text.RegularExpressions;

namespace NetMed.Infraestructure.Validator.Base
{
    public class BaseValidator 
    {
        private readonly JsonMessage _jsonMessage;
        private readonly ILoggerCustom _loggerCustom;

        public BaseValidator(ILoggerCustom loggerCustom,JsonMessage messageMapper) 
        {
            _loggerCustom = loggerCustom;
            _jsonMessage = messageMapper;
    
        }


        public OperationResult ValidateIsEntityIsNull<T>(T entity, string errorMessage)
        { 
            var result = new OperationResult();

            if (entity == null)
            {
                _loggerCustom.LogWarning(_jsonMessage.ErrorMessages["NullEntity"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NullEntity"] };

            }
            else
            {
                _loggerCustom.LogInformation(_jsonMessage.SuccessMessages["EntityFound"]);
                return new OperationResult { Success = true };
            }

        }

        public virtual OperationResult ValidateNumberEntityIsNegative(int number, string errorMessage)
        {
            var result = new OperationResult();


            if (number <= 0)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }

          
        }

        public OperationResult ValidateWithRegex(string input, string regex, string errorMessage)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new OperationResult{Success = false,Message = _jsonMessage.ErrorMessages["RegexIsNull"] };
            }

            if (!Regex.IsMatch(input, regex))
            {
                return new OperationResult{Success = false, Message = errorMessage};
            }

            return new OperationResult{Success = true, Message = _jsonMessage.SuccessMessages["RegexSuccess"] };
        }
    }
}


