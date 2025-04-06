using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Infraestructure.Validator.Implementacions
{
    public class OperationValidator : IOperationValidator
    {
        private readonly ICustomLogger _logger;
        private readonly IMessageService  _message;

        public OperationValidator(ICustomLogger logger)
        {
            _logger = logger;
            _message = new MessageService();
        }
        public OperationResult<T> CheckNull<T>(T model, string operationType)
        {
            var result = new OperationResult<T>();

            if (model == null)
            {
                result.Message = _message.GetErrorMessage(operationType);
                _logger.LogWarning(result.Message);
            }
            result.Success = true;
            return result;
        }

        public OperationResult<T> CheckId<T>(int id, string operationType)
        {
            var result = new OperationResult<T>();

            if (id <= 0)
            {
                result.Message = _message.GetErrorMessage(operationType);
                _logger.LogWarning(result.Message);
            }
            result.Success = true;
            return result;
        }

    }
}
