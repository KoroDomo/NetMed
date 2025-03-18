
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;


namespace NetMed.Application.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRespository _appointmentsRespository;
        private readonly ILogger<AppointmentsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IValidations _validations;

        public AppointmentsService(IAppointmentsRespository appointmentsRespository,  ILogger<AppointmentsService> logger, IConfiguration configuration, IValidations validations)
        {
            _appointmentsRespository = appointmentsRespository;
            _logger = logger;
            _configuration = configuration;           
            _validations = validations;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var appointments = await _appointmentsRespository.GetAllAsync();
                
            }
            catch (Exception ex )
            {
                result.Message = _configuration["AppointmentsServiceError: GetAll"];
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

                var appointments = await _appointmentsRespository.GetEntityByIdAsync(Id);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsServiceError: GetById"];
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

                var appointments = await _appointmentsRespository.RemoveAsync(Id);
                result.Success = true;
                result.Message = "Datos desactivados con exito";
            }
            catch (Exception ex) 
            {

                result.Message = _configuration["AppointmentsServiceError: Remove"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
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
                result.Message = "Datos guardados cone exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsServiceError: Save"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
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
                result.Message = "Datos actualizados cone exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsServiceError: Save"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
