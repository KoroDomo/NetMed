using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;

namespace NetMed.Persistence.Repositories
{
    public class AppointmentsRepository : BaseRepository<Appointments>, IAppointmentsRespository
    {
        private readonly NetMedContext _context;
        private readonly ILoggerSystem _logger;
        private readonly IValidations _validations;
        private readonly IMessageService _messageService;

        public AppointmentsRepository(NetMedContext context, ILoggerSystem logger, IValidations validations, IMessageService messageService) : base(context)
        {
            _context = context;
            _logger = logger;
            _validations = validations;
            _messageService = messageService;
        }

        public override async  Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            OperationResult result = new OperationResult();
            
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.Success) return result;
        
                result = _validations.AppointmentExists(entity.PatientID, entity.DoctorID, entity.AppointmentDate, entity.StatusID, (patientId, doctorId, appointmentDate,statusID) =>
                {
                    return _context.Appointments.Any(a => a.PatientID == patientId && a.DoctorID == doctorId && a.AppointmentDate == appointmentDate && a.StatusID == statusID);
                });
                if (!result.Success) return result;

                result = _validations.CheckDate(entity.AppointmentDate);
                if (!result.Success) return result;

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(SaveEntityAsync), true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(SaveEntityAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public override async Task<OperationResult> UpdateEntityAsync(Appointments entity)
        {
            OperationResult result = new OperationResult();
           
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.Success) return result;

                await base.UpdateEntityAsync(entity);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(UpdateEntityAsync), true);
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(UpdateEntityAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public override async Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {                                  
                await base.GetAllAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), true);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;          
        }
        public override async Task<OperationResult> GetAllAsync(Expression<Func<Appointments, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(filter);
                if (!result.Success) return result;

                await base.GetAllAsync(filter);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex,result.Message);
            }
            return result;
        }
        public async override Task<OperationResult> ExistsAsync(Expression<Func<Appointments, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                await base.ExistsAsync(filter);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(ExistsAsync), true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(ExistsAsync), false);
                _logger.LogError(ex, result.Message);  
            }
            return result; 
        }
        public async override Task<OperationResult> GetEntityByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.Success) return result;

                result = _validations.IsInt(Id);
                if (!result.Success) return result;
    
                result = await _validations.ExistsEntity(Id,async (id) =>
                {
                    return await _context.Appointments.AnyAsync(a => a.Id == id);
                });
                if (!result.Success) return result;

                await base.GetEntityByIdAsync(Id);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetEntityByIdAsync), true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetEntityByIdAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async override Task<OperationResult> RemoveAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(Id);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.Success) return result;

                result = await _validations.ExistsEntity(Id, async (id) =>
                {
                    return await _context.Appointments.AnyAsync(a => a.Id == id);
                });
                if (!result.Success) return result;

                await base.RemoveAsync(Id);
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(RemoveAsync), true);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(RemoveAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> CreateAppointmentAsync(int AppointmentID, int PatientID, int DoctorID, DateTime AppointmentDate, int StatusID)
        {
            OperationResult result = new OperationResult();
            
            try
            {
                result = _validations.IsNullOrWhiteSpace(PatientID);
                if (!result.Success) return result;

                result = _validations.IsInt(PatientID);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

                result = _validations.AppointmentExists(PatientID, DoctorID, AppointmentDate, StatusID, (patientId, doctorId, appointmentDate, statusID) =>
                {
                    return _context.Appointments.Any(a => a.PatientID == patientId && a.DoctorID == doctorId && a.AppointmentDate == appointmentDate && a.StatusID == statusID);
                });
                if (!result.Success) return result;

                var newAppointment = new Appointments 
                {
                    Id = 0,
                    PatientID = PatientID,
                    DoctorID = DoctorID,
                    AppointmentDate = AppointmentDate,
                    StatusID = StatusID
                };
                _context.Appointments.Add(newAppointment); 
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(CreateAppointmentAsync), true);
                result.Data = newAppointment;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(CreateAppointmentAsync), false);
                _logger.LogError(ex , result.Message);
            }
            return result;
        }     
        public async Task<OperationResult> GetAppointmentsByPatientAsync(int PatientID)
        {
            OperationResult result = new OperationResult();          
            try
            {               
                result = _validations.IsNullOrWhiteSpace(PatientID);
                if (!result.Success) return result;

                result = _validations.IsInt(PatientID);
                if (!result.Success) return result;

                result = await _validations.PatientExists(PatientID, async (id) =>
                {
                    return await _context.Appointments.AnyAsync(p => p.Id == id);
                });
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.PatientID == PatientID).ToListAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByPatientAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByPatientAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByDoctorAsync(int DoctorID)
        {  
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;
               
                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = await _validations.ExistsEntity(DoctorID, async (id) =>
                {
                    return await _context.Appointments.AnyAsync(a => a.DoctorID == id);
                });
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID).ToListAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByDoctorAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByDoctorAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        } 
        public async Task<OperationResult> GetAppointmentsByStatusAsync(int StatusID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(StatusID);
                if (!result.Success) return result;

                result = _validations.IsInt(StatusID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.StatusID == StatusID).ToListAsync();

                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByStatusAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByStatusAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByDateAsync(DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(AppointmentDate);
                if (!result.Success) return result;

                result = _validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

               var appointments = await _context.Appointments.Where(a => a.AppointmentDate == AppointmentDate).ToListAsync();

                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByDateAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByDateAsync), false);
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
        public async Task<OperationResult> CancelAppointmentAsync(int AppointmentID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(AppointmentID);
                if (!result.Success) return result;

                result = _validations.IsInt(AppointmentID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.FindAsync(AppointmentID);
                appointments.StatusID = 2;
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(CancelAppointmentAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(CancelAppointmentAsync), false);
                _logger.LogError(ex , result.Message);

            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByDoctorAndDateAsync(int DoctorID, DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID && a.AppointmentDate == AppointmentDate).ToListAsync();
                result.Success = true;
                result.Message = _messageService.GetMessage(nameof(GetAppointmentsByDoctorAndDateAsync), true);
                result.Data = appointments;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(CancelAppointmentAsync), false);
                _logger.LogError(ex , result.Message);
            }
            return result;
        }
    }
}
