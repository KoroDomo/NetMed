

using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Roles;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class RolesServices : IRolesContract
    {
        private readonly NetmedContext _context;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessageMapper;
        private readonly IRolesValidator _rolesValidator;


        public RolesServices(NetmedContext context,IRolesRepository rolesRepository,
                             ILoggerCustom logger,
                             JsonMessage jsonMessageMapper) 
        {
          _context = context;
          _rolesRepository = rolesRepository;
          _logger = logger;
          _jsonMessageMapper = jsonMessageMapper;


        }

        public async Task<OperationResult> DeleteDto(Roles dtoDelete)
        {
            try
            {
                var rolDeleted = await _rolesRepository.DeleteRoleAsync(dtoDelete);
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["RoleDeleted"], nameof(Roles), dtoDelete);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["RoleDeleted"], Data = dtoDelete };
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

        public Task<OperationResult> DeleteDto(DeleteRolesDto dtoDelete)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetAllDto()
        {
            OperationResult result = new OperationResult();

            try
            {
                var rol= await _rolesRepository.GetAllAsync();
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["GetAllEntity"], nameof(Roles), rol);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["GetAllEntity"], Data = rol };
            }


            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }

        }

        public async Task<OperationResult> GetDtoById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {

                if (!result.Success)
                {
                    _logger.LogError(result.Message);
                    return result;
                }

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["RoleFound"], nameof(Roles), id);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["RoleFound"]};

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };

            }

        }

        public Task<OperationResult> GetDtoById(Notification notification)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> SaveDto(SaveRolesDto dtoSave)
        {

            OperationResult result = new OperationResult();
            try
            {
                var rol = new Roles
                {
                    RoleName  = dtoSave.RoleName,
                    
                };

                var roles= await _rolesRepository.SaveEntityAsync(rol);
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["RoleCreated"], nameof(Roles), roles);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["RoleCreated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }

        }

        public async Task<OperationResult> UpdateDto(UpdateRolesDto dtoUpdate)
        {
            OperationResult result = new OperationResult();
            try
            {
                var roles = new Roles
                {
                    Id = dtoUpdate.RolesId,
                    RoleName = dtoUpdate.RoleName,
                    
                };

                var rol = await _rolesRepository.UpdateRoleAsync(roles);
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["RoleUpdated"], nameof(Roles), roles);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["RoleUpdated"], Data = roles};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

       
    }
}
