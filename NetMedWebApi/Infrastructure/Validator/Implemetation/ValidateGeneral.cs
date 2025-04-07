using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Models;

namespace NetMedWebApi.Infrastructure.Validator
{

    public class ValidateGeneral : IValidateGeneral
    {
        private readonly ILoggerCustom _logger;
        private readonly IJsonMessage _message;

        public ValidateGeneral(ILoggerCustom logger, IJsonMessage message)
        {
            _logger = logger;
            _message = message;
        }
        public OperationResult<T> CheckIfEntityIsNull<T>(T model, string operationType)
        {
            var result = new OperationResult<T>();

            if (model == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
            }
            result.Success = true;
            return result;
        }

        public OperationResult<T> CheckIfId<T>(int id, string operationType)
        {
            var result = new OperationResult<T>();

            if (id <= 0)
            {
                result.Message = _message.ErrorMessages["InvalidId"];
                _logger.LogWarning(result.Message);
            }
            result.Success = true;
            return result;
        }

    }
}

