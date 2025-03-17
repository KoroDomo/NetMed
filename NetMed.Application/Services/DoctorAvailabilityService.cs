using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.DoctorAvailability;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.IValidators;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;


namespace NetMed.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly ILogger<DoctorAvailabilityService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IValidations _validations;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository , ILogger<DoctorAvailabilityService> logger, IConfiguration configuration, IValidations validations)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            _configuration = configuration;
            _validations = validations;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = await _doctorAvailabilityRepository.GetAllAsync();

            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityServiceError: GetAll"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(Id);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.Success) return result;

                var doctorAvailability = await _doctorAvailabilityRepository.GetEntityByIdAsync(Id);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityServiceError: GetById"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> Remove(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.Success) return result;

                result = _validations.IsInt(Id);
                if (!result.Success) return result;

                var doctorAvailability = await _doctorAvailabilityRepository.RemoveAsync(Id);
                result.Success = true;
                result.Message = "Datos desactivados con exito";
            }
            catch (Exception ex)
            {

                result.Message = _configuration["DoctorAvailabilityServiceError: Remove"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> Save(SaveDoctorAvailabilityDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = new DoctorAvailability
                {
                    DoctorID = TDto.DoctorID,                    
                    AvailableDate = TDto.AvailableDate,
                    StartTime = TDto.StartTime,
                    EndTime = TDto.EndTime
                };
                await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvailability);
                result.Success = true;
                result.Message = "Datos guardados cone exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityServiceError: Save"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> Update(UpdateDoctorAvailabilityDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = new DoctorAvailability
                {
                    Id = TDto.AvailabilityID,
                    DoctorID = TDto.DoctorID,
                    AvailableDate = TDto.AvailableDate,
                    StartTime = TDto.StartTime,
                    EndTime = TDto.EndTime
                };
                await _doctorAvailabilityRepository.UpdateEntityAsync(doctorAvailability);
                result.Success = true;
                result.Message = "Datos actualizados cone exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityServiceError: Save"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
