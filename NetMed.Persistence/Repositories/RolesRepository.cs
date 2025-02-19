
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
        OperationResult result = new OperationResult();

        private readonly NetmedContext _context;
        private readonly ILogger<RolesRepository> _logger;
        private readonly IConfiguration _configuration;

        public RolesRepository(NetmedContext context,
                                     ILogger<RolesRepository> logger,
                                     IConfiguration configuration) : base(context)
        {


            this._context = context;
            this._logger = logger;
            this._configuration = configuration;

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
            var validationResult = EntityValidator.ValidatePositiveNumber(roleId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var roles = await _context.Roles.FindAsync(roleId);

                var notNullRoles = EntityValidator.ValidateNotNull(roleId, "El rol no a sido encontrada");


                if (!notNullRoles.Success)
                {

                    return notNullRoles;

                }
                else
                {
                    return new OperationResult { Success = true, Mesagge = "Rol obtenido con éxito", Data = roleId };

                }
            }
            catch (Exception ex)
            {
                await _context.SaveChangesAsync();
                return new OperationResult { Success = false, Mesagge = "Error al obtener el rol" };

            }

        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "El rol no puede ser creado");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.Roles.Add(roles);
                    await _context.SaveChangesAsync();

                    return new OperationResult { Success = true, Mesagge = "El rol se a creado con exito" };


                };
            }
            catch (Exception ex)
            {

                return new OperationResult { Success = false, Mesagge = "Problemas con crear el rol" };

            }

        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "El rol no se a encontrado");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            try
            {

                {
                    _context.Roles.Update(roles);
                    await _context.SaveChangesAsync();

                    return new OperationResult { Success = true, Mesagge = "El rol se actualizo con exito" };

                };
            }
            catch (Exception ex)
            {

                return new OperationResult { Success = false, Mesagge = "Problemas con actualizar el rol" };

            }

        }

        public async Task<OperationResult> DeleteRoleAsync(int rolesId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(rolesId, "No esta permitido valores negativos");

            if (!validationResult.Success)
            {

                return validationResult;

            }

            try
            {
                var roles = await _context.Roles.FindAsync(rolesId);

                var notNullRol = EntityValidator.ValidateNotNull(roles, "El rol no se a encontrado");

                if (!notNullRol.Success)
                {
                    return notNullRol;
                };

                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Mesagge = " eliminado con exito" };

            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Mesagge = "Surgieron problemas a la hora de eliminar el rol"

                };

            }
        }
    }
}
