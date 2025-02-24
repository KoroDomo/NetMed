using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class SpecialtiesService : ISpecialtiesService
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        private readonly ILogger<SpecialtiesService> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesService(ISpecialtiesRepository specialtiesRepository, 
                                    ILogger<SpecialtiesService> logger, 
                                    IConfiguration configuration)  
        {
            _specialtiesRepository = specialtiesRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var specialties = await _specialtiesRepository.GetAllAsync();
                result.Success = true;
                result.Data = specialties;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.LogError("", ex.ToString());
                return result;
            }
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
