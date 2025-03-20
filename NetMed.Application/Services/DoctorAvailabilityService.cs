
using NetMed.Application.Dtos.DoctorAvailability;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly ILoggerSystem _logger;
        private readonly IValidations _validations;
        private readonly IMessageService _messageService;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository , ILoggerSystem logger, IValidations validations, IMessageService messageService)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            _validations = validations;
            _messageService = messageService;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = await _doctorAvailabilityRepository.GetAllAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAll), true);
                result.Data = doctorAvailability;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAll), false);               
                _logger.LogError(ex, result.Message);
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
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetById), true);
                result.Data = doctorAvailability;        
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message= _messageService.GetMessage(nameof(GetById), false);
                _logger.LogError(ex, result.Message);
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
                result.Message = _messageService.GetMessage(nameof(Remove), true);
                result.Data= doctorAvailability;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(Remove), false);
                _logger.LogError(ex, result.Message);
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
                result.Message = _messageService.GetMessage(nameof(Save), true);
                result.Data = doctorAvailability;   
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(Save), false);
                _logger.LogError(ex, result.Message);
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
                result.Message = _messageService.GetMessage(nameof(Update), true);
                result.Data = doctorAvailability;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(Update), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        
    }
}
