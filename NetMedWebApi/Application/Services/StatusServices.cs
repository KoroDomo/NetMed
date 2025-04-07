using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Status;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Application.Services
{

    public class StatusServices : IStatusContract
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILoggerCustom _logger;
        private readonly IJsonMessage _message;

        public StatusServices(IStatusRepository status, ILoggerCustom logger, IJsonMessage message)
        {
            _statusRepository = status;
            _logger = logger;
            _message = message;

        }

        public async Task<OperationResult<SaveStatusModel>> Create(SaveStatusModel dto)
        {
            var result = new OperationResult<SaveStatusModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _statusRepository.CreateStatusAsync(dto);

                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<DeleteStatusModel>> Delete(DeleteStatusModel dto)
        {
            var result = new OperationResult<DeleteStatusModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {

                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }

                _logger.LogWarning(result.Message);
                result = await _statusRepository.DeleteStatusAsync(dto);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResultList<StatusApiModel>> GetAll()
        {
            var result = new OperationResultList<StatusApiModel>();

            try
            {
                var repositoryResult = await _statusRepository.GetAllStatusAsync();

                if (!repositoryResult.Success)
                {
                    result.Message = _message.ErrorMessages["NullEntity"];
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogWarning(result.Message);
                return repositoryResult;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GetAllError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<T>> GetById<T>(int Id)
        {
            var result = new OperationResult<T>();

            try
            {
                result = await _statusRepository.GetStatusByIdAsync<T>(Id);
                if (!result.Success)
                {
                    result.Message = _message.ErrorMessages["NullEntity"];
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogWarning(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GetAllError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<UpdateStatusModel>> Update(UpdateStatusModel dto)
        {
            var result = new OperationResult<UpdateStatusModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _statusRepository.UpdateStatusAsync(dto);

                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    result.Message = _message.ErrorMessages[""];
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["CreateError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
