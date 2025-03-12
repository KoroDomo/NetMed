using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
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
        private readonly ILogger<RolesRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly JsonMessage _jsonMessage;

        public RolesRepository(NetmedContext context,
                               ILogger<RolesRepository> logger,
                               JsonMessage jsonMessage) : base(context, logger, jsonMessage)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = jsonMessage;
        }

        public override Task<List<Roles>> GetAllAsync()
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync();
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Roles, bool>> filter)
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetRoleByIdAsync(int roleId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(roleId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(roleId);

                var notNullRole = EntityValidator.ValidateNotNull(role, _jsonMessage.ErrorMessages["RoleNotFound"]);

                if (!notNullRole.Success)
                {
                    _logger.LogWarning(notNullRole.Message);
                    return notNullRole;
                }

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
            var validationResult = EntityValidator.ValidateNotNull(roles, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                await _context.Roles.AddAsync(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityCreated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityCreated"], Data = roles };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityUpdated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityUpdated"], Data = roles };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int rolesId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(rolesId, _jsonMessage.ErrorMessages["InvalidId"]);

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

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityDeleted"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityDeleted"], Data = role };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
    }
}