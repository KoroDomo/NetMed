using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly NetmedContext _context;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessage;
        private readonly IRolesValidator _rolesValidator;

        public RolesRepository(NetmedContext context,
                               ILoggerCustom logger,
                               JsonMessage jsonMessage) : base(context)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = jsonMessage;
            _rolesValidator = new RolesValidator(logger, jsonMessage);
        }

        public override Task<OperationResult> GetAllAsync()
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync();
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Roles, bool>> filter)
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetRoleByIdAsync(int rolesId)
        {
           

            var validationResult = _rolesValidator.ValidateRolesIdIsNegative(rolesId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(rolesId);

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleFound"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleFound"], Data = role };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {

            var validationResult = _rolesValidator.ValidateRoleIsNotNull(roles, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            var idValidationResult = _rolesValidator.ValidateRolesIdIsNegative(roles.Id, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!idValidationResult.Success)
            {
                _logger.LogError(idValidationResult.Message);
                return idValidationResult;
            }

            try
            {
                await _context.Roles.AddAsync(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleCreated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleCreated"], Data = roles };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {

            var validationResult = _rolesValidator.ValidateRoleIsNotNull(roles, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            var validateNotNull = _rolesValidator.ValidateRolesIdIsNegative(roles.Id, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validateNotNull.Success)
            {
                _logger.LogError(validateNotNull.Message);
                return validateNotNull;
            }

            try
            {
                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleUpdated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleUpdated"], Data = roles };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int rolesId)
        {

            var validationResult = _rolesValidator.ValidateRolesIdIsNegative(rolesId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(rolesId);

                var notNullRole = EntityValidator.ValidateNotNull(role, _jsonMessage.ErrorMessages["RoleNotFound"]);

                if (!notNullRole.Success)
                {
                    _logger.LogWarning(notNullRole.Message);
                    return notNullRole;
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleDeleted"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleDeleted"], Data = role };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
    }
}