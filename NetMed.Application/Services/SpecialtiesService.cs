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
                if (!specialties.Success)
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
                result.Message = "Error obteniendo la Especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }


        public Task<OperationResult> Remove(RemoveSpecialtiesDto dto)
        {
            throw new NotImplementedException();
        }
        
        public async Task<OperationResult> Save(SaveSpecialtiesDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                Specialties specialty = new Specialties()
                {
                    SpecialtyName = dto.SpecialtyName,
                    IsActive = true,
                    
                };

                var s = await _specialtiesRepository.SaveEntityAsync(specialty);

                result.Success = s.Success;
                result.Message = s.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error guardando la Especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateSpecialtiesDto dto)
        {

            OperationResult result = new OperationResult();
            try
            {
                var resultGetById = await _specialtiesRepository.GetEntityByIdAsync(dto.SpecialtyID);

                if (!resultGetById.Success)
                {
                    result.Success = resultGetById.Success;
                    result.Message = resultGetById.Message;
                    return result;
                }

                Specialties? specialty = new Specialties()
                {
                    Id = (short)dto.SpecialtyID,
                    SpecialtyName = dto.SpecialtyName,
                    IsActive = dto.IsActive
                };
                var s = await _specialtiesRepository.UpdateEntityAsync(specialty);
                result.Success = s.Success;
                result.Message = s.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error actualizando la Especialidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result; 
        }
    }
}
