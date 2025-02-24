
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

        public override Task<List<Roles>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

      

        public override Task<OperationResult> GetAllAsync(Expression<Func<Roles, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetRoleByIdAsync(int roleId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(roleId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(roleId);

                var notNullRole = EntityValidator.ValidateNotNull(role, "No se encontró el rol");

                if (!notNullRole.Success)
                {
                    return notNullRole;
                }

                return new OperationResult { Success = true, Message = "Rol obtenido con éxito ", Data = role };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "La entidad 'Roles' no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                await _context.Roles.AddAsync(roles);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Rol creado con éxito", Data = roles };
            }
            catch (Exception ex)
            {
                return new OperationResult
                { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "La entidad no puede ser nula");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                _context.Roles.Update(roles);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Rol actualizado con éxito " , Data = roles};
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int rolesId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(rolesId, "El ID proporcionado no es válido");

            if (!validationResult.Success)
            {
                return validationResult;
            }

            try
            {
                var role = await _context.Roles.FindAsync(rolesId);

                var notNullRole = EntityValidator.ValidateNotNull(role, "No se encontró el rol");

                if (!notNullRole.Success)
                {
                    return notNullRole;
                }

                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Rol eliminado con éxito" , Data = role};
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error al procesar la solicitud" };
            }
        }
    }
}
  