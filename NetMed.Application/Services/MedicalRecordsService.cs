using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;
        private readonly ILogger<MedicalRecordsService> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsService(IMedicalRecordsRepository medicalRecordsRepository,
                                    ILogger<MedicalRecordsService> logger,
                                    IConfiguration configuration) 
        {
            _medicalRecordsRepository = medicalRecordsRepository;
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
