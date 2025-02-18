
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
            var result = new OperationResult();

            if (roleId < 0)
            {
                result.Success = false;
                result.Mesagge = "ID no válido.";
                return result;
            }

            try
            {
                var roles = await _context.Roles
                    .FirstOrDefaultAsync(n => n.Id == roleId);

                if (roles == null)
                {
                    result.Success = false;
                    result.Mesagge = "Rol no encontrado.";
                }
                else
                {
                    result.Success = true;
                    result.Mesagge = "Rol obtenida con éxito.";
                    result.Data = roles;
                }
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Mesagge = $"Error al obtener el rol: {ex.Message}";
                await _context.SaveChangesAsync();
            }
            return result;


        }

        public async  Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "Notification");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Roles.Add(roles);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Mesagge = "El rol se a creado con exito";

                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Problemas con crear el rol";

            }

            return result;
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "Roles");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Roles.Update(roles);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Mesagge = "El rol se actualizo con exito";

                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Problemas con actualizar el rol";

            }

            return result;

        }

        public async Task<OperationResult> DeleteRoleAsync(Roles roles)
        {
            var validationResult = EntityValidator.ValidateNotNull(roles, "Roles");


            if (!validationResult.Success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            try
            {

                {
                    _context.Roles.Remove(roles);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Mesagge = "El rol se elimino con exito";

                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Mesagge = "Problemas eliminando el rol";

            }

            return result;


        }

        public Task<OperationResult> GetAllRolesAsync(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
