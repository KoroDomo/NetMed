
using NetMed.Application.Dtos;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Interfaces;


namespace NetMed.Application.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRespository _appointmentsRespository;
        private readonly ILoggerSystem _logger;
        private readonly IValidations _validations;
        private readonly IMessageService _messageService;

        public AppointmentsService(IAppointmentsRespository appointmentsRespository, ILoggerSystem logger, IValidations validations, IMessageService messageService)
        {
            _appointmentsRespository = appointmentsRespository;
            _logger = logger;          
            _validations = validations;
            _messageService = messageService;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = await _appointmentsRespository.GetAllAsync();
                var appointmentDtos = appointments.ToDtoList();

                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAll), true); 
                result.Data = appointmentDtos;
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

                var appointments = await _appointmentsRespository.GetEntityByIdAsync(Id);
                var appointmentDtos = appointments.ToDto();

                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetById), true);
                result.Data = appointmentDtos;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetById), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> Remove(RemoveAppointmentsDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(TDto);
                if (!result.Success) return result;
                
                result = _validations.IsInt(TDto);
                if (!result.Success) return result;

                var appointments = new Appointments
                {
                    Id = TDto.AppointmentID,                  
                };
                var data = await _appointmentsRespository.RemoveAsync(appointments);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(Remove), true);
                result.Data = data;
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(Remove), true);
                _logger.LogError(ex, result.Message);
            }
            return result;          
        }
        public async Task<OperationResult> Save(SaveAppointmentsDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = new Appointments
                {
                    PatientID = TDto.PatientID,
                    DoctorID = TDto.DoctorID,
                    AppointmentDate = TDto.AppointmentDate,
                    StatusID = TDto.StatusID
                };
                await _appointmentsRespository.SaveEntityAsync(appointments);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(Save), true); 
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(Save), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> Update(UpdateAppointmentsDto TDto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = new Appointments
                {
                    Id = TDto.AppointmentID,
                    PatientID = TDto.PatientID,
                    DoctorID = TDto.DoctorID,
                    AppointmentDate = TDto.AppointmentDate,
                    StatusID = TDto.StatusID
                };
                await _appointmentsRespository.UpdateEntityAsync(appointments);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(Update), true);  
                result.Data = appointments;
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
