using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class AvailabilityModesService : IAvailabilityModesService
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;
        private readonly ILogger<AvailabilityModesService> _logger;
        private readonly IConfiguration _configuration;

        public AvailabilityModesService(IAvailabilityModesRepository availabilityModesRepository,
                                        ILogger<AvailabilityModesService> logger,
                                        IConfiguration configuration) 
        {
            _availabilityModesRepository = availabilityModesRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
