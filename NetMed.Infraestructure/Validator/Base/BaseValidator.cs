using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Infrastructure.Validator.Interfaces;
using System.Text.RegularExpressions;

namespace NetMed.Infraestructure.Validator.Base
{
    public class BaseValidator<TEntity> : IBaseValidator<TEntity> where TEntity : class
    {
        private readonly JsonMessage _jsonMessage;
        private readonly ILoggerCustom _loggerCustom;

        public BaseValidator(ILoggerCustom loggerCustom,JsonMessage messageMapper) 
        {
            _loggerCustom = loggerCustom;
            _jsonMessage = messageMapper;
    
        }

       

        public OperationResult IsNullOrWhiteSpace<TEntity1>(TEntity1 entity)
        {
            OperationResult result = new OperationResult();

            if (entity is string se && string.IsNullOrWhiteSpace(se))
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NullEntity"] };
            }
            else 
            {
                return new OperationResult { Success = true };
            }

        }

       

        public OperationResult ValidateIsEntityIsNull(TEntity entity, string errorMessageKey)
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

            if (number is int intValue)
            {

                if (number <= 0)
                {
                    return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };
                }
                else
                {
                    return new OperationResult { Success = true };
                }
            }
            else
            { 
             return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };
            }
        }

        public OperationResult ValidateStringLength(string entityName, int maxLength)
        {
            OperationResult result = new OperationResult();

            if (entityName.Length > maxLength)
            {

                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["NotificationMessage"] };
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

        public OperationResult ValideteIdIsNotNull(int ID)
        {
            OperationResult resul = new OperationResult();

            if (ID == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidNullId"] };

            }
            else 
            { 
            
             return new OperationResult { Success = true };
            }
        
        }
    }
}


