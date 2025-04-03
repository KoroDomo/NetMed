using NetMed.Application.Dtos.DoctorAvailability;
using NetMed.Application.Dtos.Extensions;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
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
                var doctorAvailabilityDto = doctorAvailability.ToDtoList();

                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetAll), true);
                result.data = doctorAvailabilityDto;

            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(GetAll), false);               
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(Id);
                if (!result.success) return result;

                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.success) return result;

                var doctorAvailability = await _doctorAvailabilityRepository.GetEntityByIdAsync(Id);
                var doctorAvailabilityDto = doctorAvailability.ToDto();

                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetById), true);
                result.data = doctorAvailabilityDto;        
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message= _messageService.GetMessage(nameof(GetById), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> Remove(RemoveDoctorAvailabilityDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(TDto);
                if (!result.success) return result;

                result = _validations.IsInt(TDto);
                if (!result.success) return result;


                var doctorAvailability = await _doctorAvailabilityRepository.GetEntityByIdAsync(TDto.availabilityID);
                var doctorAvailabilityDto = doctorAvailability.ToDto();

                await _doctorAvailabilityRepository.RemoveAsync(TDto.availabilityID);

                result.success = true;
                result.message = _messageService.GetMessage(nameof(Remove), true);
                result.data= doctorAvailabilityDto;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(Remove), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> Save(SaveDoctorAvailabilityDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = TDto.ConvertToEntitySave();

                await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvailability);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(Save), true);
                result.data = doctorAvailability;   
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(Save), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> Update(UpdateDoctorAvailabilityDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctorAvailability = TDto.ConvertToEntityUpdate();

                await _doctorAvailabilityRepository.UpdateEntityAsync(doctorAvailability);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(Update), true);
                result.data = doctorAvailability;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(Update), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }       
    }
}
