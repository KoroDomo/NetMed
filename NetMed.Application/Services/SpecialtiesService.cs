using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
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
                var specialties = await _specialtiesRepository.GetAll();
                
                if(!specialties.Success)
                {
                    result.Success = false;
                    result.Message = specialties.Message;
                    return result;
                }
                result.Data = specialties.Data;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo las Especialidades";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var specialties = await _specialtiesRepository.GetEntityByIdAsync(Id);
                if (specialties == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró la especialidad";
                    return result;
                }
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
        
        public async Task<OperationResult> Remove(RemoveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }
        
        public async Task<OperationResult> Save(SaveSpecialtiesDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                Specialties specialty = new()
                {
                    SpecialtyName = dto.SpecialtyName,
                    IsActive = dto.IsActive
                };

                var specialties = await _specialtiesRepository.SaveEntityAsync(specialty);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.LogError("", ex.ToString());
                return result;
            }

            return result;
        }

        public Task<OperationResult> Update(UpdateSpecialtiesDto dto)
        {
                
            throw new NotImplementedException();
        }
    }
}
