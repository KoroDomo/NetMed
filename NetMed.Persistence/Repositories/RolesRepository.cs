
using Microsoft.EntityFrameworkCore;
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

        public RolesRepository(NetmedContext context,
                               ILogger<RolesRepository> logger,
                               IConfiguration configuration) : base(context, logger, configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override Task<Roles> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }

        public override Task<OperationResult> SaveEntityAsync(Roles entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<List<Roles>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<OperationResult> UpdateEntityAsync(Roles entity)
        {
            return base.UpdateEntityAsync(entity);
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Roles, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetRoleByIdAsync(int roleId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(roleId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "roleId", roleId);
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(roleId);

                var notNullRole = EntityValidator.ValidateNotNull(role, _configuration["ErrorMessages:RoleNotFound"]);

                if (!notNullRole.Success)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:RoleNotFound"], "roleId", roleId);
                    return notNullRole;
                }

                _logger.LogInformation(_configuration["ErrorMessages:RoleRetrieved"], "roleId", roleId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:RoleRetrieved"], Data = role };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "roles", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.Roles.Add(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:RoleCreated"], "roleId", roles.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:RoleCreated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "roles", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:RoleUpdated"], "roleId", roles.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:RoleUpdated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int rolesId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(rolesId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "rolesId", rolesId);
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(rolesId);

                var notNullRole = EntityValidator.ValidateNotNull(role, _configuration["ErrorMessages:RoleNotFound"]);

                if (!notNullRole.Success)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:RoleNotFound"], "rolesId", rolesId);
                    return notNullRole;
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:RoleDeleted"], "rolesId", rolesId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:RoleDeleted"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }
    }
}
