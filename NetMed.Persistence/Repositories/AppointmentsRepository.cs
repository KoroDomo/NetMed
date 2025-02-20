using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class AppointmentsRepository : BaseRepository<Appointments>, IAppointmentsRespository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsRepository(NetMedContext context, ILogger<AppointmentsRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public override async Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            var validationResult = EntityValidator.Validator(entity, nameof(entity));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.SaveEntityAsync(entity);
        }
        public override async Task<OperationResult> UpdateEntityAsync(Appointments entity)
        {
            var validationResult = EntityValidator.Validator(entity, nameof(entity));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.UpdateEntityAsync(entity);
        }
        public override async Task<List<Appointments>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }
        public override async Task<OperationResult> GetAllAsync(Expression<Func<Appointments, bool>> filter)
        {
            var validationResult = EntityValidator.Validator(filter, nameof(filter));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.GetAllAsync(filter);
        }
        public async override Task<bool> ExistsAsync(Expression<Func<Appointments, bool>> filter)
        {
            var validationResult = EntityValidator.Validator(filter, nameof(filter));
            if (!validationResult.Success)
            {
               await Task.FromResult(false);
            }
            return await base.ExistsAsync(filter);
        }
        public async override Task<Appointments> GetEntityByIdAsync(int id)
        {
            var validationResult = EntityValidator.Validator(id, nameof(id));
            if (!validationResult.Success)
            {
                await Task.FromResult(false);
            }
            return await base.GetEntityByIdAsync(id);
        }

        public async Task<OperationResult> CreateAppointmentAsync(int PatientID, int DoctorID, DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            var patientIdResult = EntityValidator.Validator(PatientID, nameof(PatientID));
            if (!patientIdResult.Success) return patientIdResult;

            var doctorIdResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIdResult.Success) return doctorIdResult;

            if (AppointmentDate >= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            try
            {
                var newAppointment = new Appointments 
                {
                    PatientID = PatientID,
                    DoctorID = DoctorID,
                    AppointmentDate = AppointmentDate                    
                };
                _context.Appointments.Add(newAppointment); 
                await _context.SaveChangesAsync();
                return new OperationResult { Success = true, Message = "Cita creada exitosamente.", Data = newAppointment.Id };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: CreateAppointmentAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetAppointmentByIdAsync(int AppointmentID)
        {
            OperationResult result = new OperationResult();
            var appointmentIDResult = EntityValidator.Validator(AppointmentID, nameof(AppointmentID));
            if (!appointmentIDResult.Success) return appointmentIDResult;
            try
            {
                var appointment = await _context.Appointments.FindAsync(AppointmentID); 
                if (appointment is null)
                {
                    return new OperationResult { Success = false, Message = "No se encontró la cita." };
                }
                return new OperationResult { Success = true, Data = appointment };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentByIdAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByPatientAsync(int PatientID)
        {
            OperationResult result = new OperationResult();
            var patientIDResult = EntityValidator.Validator(PatientID, nameof(PatientID));
            if (!patientIDResult.Success) return patientIDResult;
            try
            {
                var appointments = await _context.Appointments.Where(a => a.PatientID == PatientID).ToListAsync();
                if (appointments is null)
                {
                    return new OperationResult { Success = false, Message = "No se encontró ninguna cita con ese ID." };
                }
                return new OperationResult { Success = true, Data = appointments };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByPatientAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByDoctorAsync(int DoctorID)
        {  
            OperationResult result = new OperationResult();
            var doctorIDResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIDResult.Success) return doctorIDResult;

            try
            {
                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID).ToListAsync();
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByDoctorAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> UpdateAppointmentStatusAsync(int AppointmentID, int StatusID)
        {
            OperationResult result = new OperationResult();
            var appointmentIDResult = EntityValidator.Validator(AppointmentID, nameof(AppointmentID));
            if (!appointmentIDResult.Success) return appointmentIDResult;

            var statusIDResult = EntityValidator.Validator(StatusID, nameof(StatusID));
            if (!statusIDResult.Success) return statusIDResult;

            try
            {
                var appointment = await _context.Appointments.FindAsync(AppointmentID);
                if (appointment == null)
                {
                    return new OperationResult { Success = false, Message = "Cita no encontrada." };
                }
                appointment.StatusID = StatusID; 
                await _context.SaveChangesAsync();
                return new OperationResult { Success = true, Message = "Estado de la cita actualizado." };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: UpdateAppointmentStatusAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByStatusAsync(int StatusID)
        {
            OperationResult result = new OperationResult();
            var statusIDResult = EntityValidator.Validator(StatusID, nameof(StatusID));
            if (!statusIDResult.Success) return statusIDResult;

            try
            {
                var appointments = await _context.Appointments.Where(a => a.StatusID == StatusID).ToListAsync();
                if (appointments == null || appointments.Count == 0) 
                {
                    return new OperationResult { Success = false, Message = "No se encontraron citas con ese estado." }; 
                }
                return new OperationResult { Success = true, Data = appointments };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByStatusAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetAppointmentsByDateAsync(DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            if (AppointmentDate >= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            try
            {
                var appointments = await _context.Appointments.Where(a => a.AppointmentDate == AppointmentDate).ToListAsync();
                return new OperationResult { Success = true, Data = appointments };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByDateAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> CancelAppointmentAsync(int AppointmentID)
        {
            OperationResult result = new OperationResult();
            var appointmentIDResult = EntityValidator.Validator(AppointmentID, nameof(AppointmentID));
            if (!appointmentIDResult.Success) return appointmentIDResult;

            try
            {

                var appointments = await _context.Appointments.FindAsync(AppointmentID);
                if (appointments == null)
                {
                    return new OperationResult { Success = false, Message = "Cita no encontrada." };
                }
                appointments.StatusID = 2;
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Cita cancelada." };
            }
            catch (Exception ex )
            {
                result.Message = _configuration["AppointmentsRepositoryError: CancelAppointmentAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetAppointmentsByDoctorAndDateAsync(int DoctorID, DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            var doctorIDResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIDResult.Success) return doctorIDResult;

            if (AppointmentDate >= DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            try
            {
                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID && a.AppointmentDate == AppointmentDate).ToListAsync();
                return new OperationResult { Success = true, Data = appointments };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByDoctorAndDateAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
