using NetMedWebApi.Application.Contracts;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Roles;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Application.Services
{
    public class RolesServices : IRolesContract
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly ILoggerCustom _logger;
        private readonly IJsonMessage _message;

        public RolesServices(IRolesRepository roles, ILoggerCustom logger, IJsonMessage message)
        {
            _rolesRepository = roles;
            _logger = logger;
            _message = message;

        }

        public async Task<OperationResult<SaveRolesModel>> Create(SaveRolesModel dto)
        {
            var result = new OperationResult<SaveRolesModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _rolesRepository.CreateRoleAsync(dto);

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

        public async Task<OperationResult<T>> GetById<T>(int Id)
        {
            var result = new OperationResult<T>();

            try
            {
                result = await _rolesRepository.GetRoleByIdAsync<T>(Id);
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

        public async Task<OperationResultList<RolesApiModel>> GetAll()
        {
            var result = new OperationResultList<RolesApiModel>();

            try
            {
                var repositoryResult = await _rolesRepository.GetAllRolesAsync();

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

        public async Task<OperationResult<UpdateRolesModel>> Update(UpdateRolesModel dto)
        {
            var result = new OperationResult<UpdateRolesModel>();
            if (dto == null)
            {
                result.Message = _message.ErrorMessages["NullEntity"];
                _logger.LogWarning(result.Message);
                return result;
            }

            try
            {
                _logger.LogWarning(result.Message);
                result = await _rolesRepository.UpdateRoleAsync(dto);

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

        public async Task<OperationResult<DeleteRolesModel>> Delete(DeleteRolesModel dto)
        {
            var result = new OperationResult<DeleteRolesModel>();
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
                result = await _rolesRepository.DeleteRoleAsync(dto);
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
