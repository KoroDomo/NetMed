using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Roles;
using NetMed.Application.Interfaces;
using NetMed.Application.Mapper;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class RolesService : IRolesContract
    {
        private readonly NetmedContext _context;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessage;
        private readonly IRolesValidator _rolesValidator;

        public RolesService(NetmedContext context,
                            IRolesRepository rolesRepository,
                            ILoggerCustom logger,
                            JsonMessage jsonMessage)
        {
            _context = context;
            _rolesRepository = rolesRepository;
            _logger = logger;
            _jsonMessage = jsonMessage;
            _rolesValidator = new RolesValidator(logger, jsonMessage);
        }

        public async Task<OperationResult> GetAllDto()
        {
            var result = new OperationResult();
            try
            {
                var repositoryResult = await _rolesRepository.GetAllAsync();
                if (!repositoryResult.Success || repositoryResult.Data == null)
                {
                    _logger.LogInformation(_jsonMessage.ErrorMessages["GetAllEntity"], "Roles");
                    result.Success = false;
                    result.Message = _jsonMessage.ErrorMessages["GetAllEntity"];
                    return result;
                }

                var roles = (IEnumerable<Roles>)repositoryResult.Data;
                var roleDtos = RolesMapper.ToDtoList(roles);

                result.Success = true;
                result.Message = _jsonMessage.SuccessMessages["GetAllEntity"];
                result.Data = roleDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessage.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> GetDtoById(int roleId)
        {
            var result = new OperationResult();
            try
            {
                var roles = await _rolesRepository.GetRoleByIdAsync(roleId);
                var rolesDto = RolesMapper.ToDto(roles.Data);
                return result.Data = roles;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> SaveDto(SaveRolesDto dtoSave)
        {
            var result = new OperationResult();
            try
            {
                var role = new Roles
                {
                    RoleName = dtoSave.RoleName
                };

                var validationResult = _rolesValidator.ValidateRoleIsNotNull(role, _jsonMessage.ErrorMessages["NullEntity"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _rolesRepository.CreateRoleAsync(role);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var savedRole = (Roles)repositoryResult.Data;
                var roleDto = RolesMapper.ToDto(savedRole);

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleCreated"], nameof(Roles), savedRole.Id);
                result.Success = true;
                result.Message = _jsonMessage.SuccessMessages["RoleCreated"];
                result.Data = roleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessage.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> UpdateDto(UpdateRolesDto dtoUpdate)
        {
            var result = new OperationResult();
            try
            {
                var validationResult = _rolesValidator.ValidateRolesIdIsNegative(dtoUpdate.id, _jsonMessage.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var role = new Roles
                {
                    Id = dtoUpdate.id,
                    RoleName = dtoUpdate.RoleName
                };

                var repositoryResult = await _rolesRepository.UpdateRoleAsync(role);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var updatedRole = (Roles)repositoryResult.Data;
                var roleDto = RolesMapper.ToDto(updatedRole);

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleUpdated"], nameof(Roles), updatedRole.Id);
                result.Success = true;
                result.Message = _jsonMessage.SuccessMessages["RoleUpdated"];
                result.Data = roleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessage.ErrorMessages["DatabaseError"];
            }
            return result;
        }

        public async Task<OperationResult> DeleteDto(int roleId)
        {
            var result = new OperationResult();
            try
            {
                var validationResult = _rolesValidator.ValidateRolesIdIsNegative(roleId, _jsonMessage.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _rolesRepository.DeleteRoleAsync(roleId);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleDeleted"], nameof(Roles));
                result.Success = true;
                result.Message = _jsonMessage.SuccessMessages["RoleDeleted"];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessage.ErrorMessages["DatabaseError"];
            }
            return result;
        }
    }
}