
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Status;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class StatusServices : IStatusContract
    {
        private readonly NetmedContext _context;
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusServices> _logger;
        private readonly IConfiguration _configuration;

        public StatusServices(NetmedContext context, IStatusRepository statusRepository,
                                                     ILogger<StatusServices> logger ,
                                                     IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _statusRepository = statusRepository;
            _context = context;

        }



        public Task<OperationResult> DeleteDto(DeleteStatusDto dtoDelete)
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

        public Task<OperationResult> SaveDto(SaveStatusDto dtoSave)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateDto(UpdateStatusDto dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
