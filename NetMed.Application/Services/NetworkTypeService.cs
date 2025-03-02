
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{

    public class NetworktypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ILogger<NetworktypeService> _logger;
        private readonly IConfiguration _configuration;
        private readonly OperationValidator _operations;
        public NetworktypeService(INetworkTypeRepository repository,
                                  ILogger<NetworktypeService> logger, IConfiguration configuration)
        {
            _networkTypeRepository = repository;
            _logger = logger;
            _configuration = configuration;
            _operations = new OperationValidator(_configuration);
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetNetworkTypeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveNetworkTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateNetworkTypeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
