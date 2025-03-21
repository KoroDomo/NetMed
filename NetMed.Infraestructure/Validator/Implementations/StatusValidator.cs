

using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Base;
using NetMed.Infraestructure.Validator.Interfaz;

namespace NetMed.Infraestructure.Validator.Implementations
{
    public class StatusValidator : BaseValidator, IStatusValidator
    {
        private readonly JsonMessage _jsonMessage;
        private readonly ILoggerCustom _loggerCustom;

        public StatusValidator(ILoggerCustom loggerCustom,
               JsonMessage messageMapper) : base(loggerCustom, messageMapper)
        {
            _loggerCustom = loggerCustom;
            _jsonMessage = messageMapper;
        }

        public OperationResult ValidateIsEntityIsNull(Status entity, string errorMessageKey)
        {
            if (entity == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["StatusNull"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }

        public OperationResult ValidateIsStatusIdNotIsNegative(int statusId, string errorMessage)
        {
            OperationResult result = new OperationResult();

            if (statusId < 0)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["InvalidId"] };
            }

            else
            {
                return new OperationResult { Success = true };
            }

        }

        public OperationResult ValidateStatusIsNotNull(Status status, string erroMessage)
        {
            if (status == null)
            {
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["StatusNull"] };
            }
            else
            {
                return new OperationResult { Success = true };
            }
        }

       
    }
}
