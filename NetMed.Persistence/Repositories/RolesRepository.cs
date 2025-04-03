using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
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

        public override async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var roles = await base.GetAllAsync();
                _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult
                {
                    Success = false,
                    Message = _jsonMessage.ErrorMessages["DatabaseError"]
                };
            }
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Roles, bool>> filter)
        {
            try
            {
                var roles = await base.GetAllAsync(filter);
                _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult
                {
                    Success = false,
                    Message = _jsonMessage.ErrorMessages["DatabaseError"]
                };
            }
        }

        public async Task<OperationResult> GetRoleByIdAsync(int rolesId)
        {
            var validationResult = _rolesValidator.ValidateNumberEntityIsNegative(rolesId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var notification = await _context.Roles.FindAsync(rolesId);

                if (notification == null)
                {
                    _logger.LogWarning(_jsonMessage.ErrorMessages["RoleNotFound"], rolesId);
                    return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["RoleNotFound"], Data = notification };
                }

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleFound"], nameof(Notification), rolesId);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleFound"], Data = notification };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            var nullValidationResult = _rolesValidator.ValidateRoleIsNotNull(roles, _jsonMessage.ErrorMessages["NotificationNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            var idValidationResult = _rolesValidator.ValidateRolesIdIsNegative(roles.Id, _jsonMessage.ErrorMessages["RolesLessZero"]);

            if (!idValidationResult.Success)
            {
                _logger.LogError(idValidationResult.Message);
                return idValidationResult;
            }

            try
            {
                await _context.Roles.AddAsync(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleCreated"], nameof(Roles), roles.Id);

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
            var nullValidationResult = _rolesValidator.ValidateRoleIsNotNull(roles, _jsonMessage.ErrorMessages["RolesNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            var validationResult = _rolesValidator.ValidateRolesIdIsNegative(roles.Id, _jsonMessage.ErrorMessages["RolesLessZero"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleUpdated"], nameof(roles), roles.Id);
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
            var nullValidationResult = _rolesValidator.ValidateNumberEntityIsNegative(rolesId, _jsonMessage.ErrorMessages["RolesNull"]);
            if (!nullValidationResult.Success)
            {
                _logger.LogError(nullValidationResult.Message);
                return nullValidationResult;
            }

            try
            {
                var roles = await _context.Roles.FindAsync(rolesId);


                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["RoleDeleted"], nameof(Roles), roles);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["RoleDeleted"], Data = roles };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
    }
}
