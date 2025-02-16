
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
                result.success = false;
                result.Mesagge = "ID no valido";
            }
            else
            {
                result.success = true;
                result.Mesagge = "Rol encontrado con exito";

            }
            return result;

        }

        public Task<OperationResult> CreateRoleAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles role)
        {
            var validationResult = EntityValidator.ValidateNotNull(role, "Roles");


            if (!validationResult.success)
            {
                return validationResult;

            }

            var result = new OperationResult();
            {
                result.success = true;
                result.Mesagge = "Se a creado un un nuevo rol con exito";

            };

            return result;

        }

        public async Task<OperationResult> DeactivateRoleAsync(int roleId)
        {
            if (roleId < 0)
            {

                result.success = false;
                result.Mesagge = "El ID no es valido";

            }
            else
            {
                result.success = true;
                result.Mesagge = "El rol se a  desactivado ";

            }
            return result ;


        }

        public Task<OperationResult> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
