

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Roles;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class RolesServices : IRolesContract
    {
        private readonly NetmedContext _context;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger<RolesServices> _logger;
        private readonly IConfiguration _configuration;

        public RolesServices(NetmedContext context,IRolesRepository rolesRepository,
                             ILogger<RolesServices> logger,
                             IConfiguration configuration) 
        {
          _context = context;
          _rolesRepository = rolesRepository;
          _logger = logger;
          _configuration = configuration;

        }


        public Task<OperationResult> DeleteDto(DeleteRolesDto dtoDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationResult>> GetAllDto()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetDtoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SaveDto(SaveRolesDto dtoSave)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateDto(UpdateRolesDto dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
