using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
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
        public override async  Task<OperationResult> SaveEntityAsync(Appointments entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(entity, nameof(entity));
                if (!result.Success) return result;

                result = Validations.AppointmentExists(entity.PatientID, entity.AppointmentDate, (patientId, appointmentDate) =>
                {
                    return _context.Appointments.Any(a => a.PatientID == patientId && a.AppointmentDate == appointmentDate);
                });
                if (!result.Success) return result;

                result = Validations.CheckDate(entity.AppointmentDate);
                if (!result.Success) return result;

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Datos Guardados con exito"; 
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message,ex.ToString());
            }
            return result;
        }
        public override async Task<OperationResult> UpdateEntityAsync(Appointments entity)
        {
            OperationResult result = new OperationResult();
           
            try
            {
                result = Validations.IsNullOrWhiteSpace(entity, nameof(entity));
                if (!result.Success) return result;

                await base.UpdateEntityAsync(entity);
                result.Success = true;
                result.Message = "Datos Actualizados con exito";
            }

            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: UpdateEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
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
                result.Message = "Datos Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAllAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;          
        }
        public override async Task<OperationResult> GetAllAsync(Expression<Func<Appointments, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(filter, nameof(filter));
                if (!result.Success) return result;

                await base.GetAllAsync(filter);
                result.Success = true;
                result.Message = "Datos Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAllAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
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
                result.Message = "Los Datos Existen";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: ExistsAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result; ;
        }
        public async override Task<OperationResult> GetEntityByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(Id, nameof(Id));
                if (!result.Success) return result;

                result = Validations.IsInt(Id);
                if (!result.Success) return result;
    
                result = await Validations.ExistsEntity(Id,async (id) =>
                {
                    return await _context.Appointments.AnyAsync(a => a.Id == id);
                });
                if (!result.Success) return result;

                await base.GetEntityByIdAsync(Id);
                result.Success = true;
                result.Message = "Datos Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetEntityByIdAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> CreateAppointmentAsync(int PatientID, int DoctorID, DateTime AppointmentDate)
        {
            OperationResult result = new OperationResult();
            
            try
            {
                result = Validations.IsNullOrWhiteSpace(PatientID, nameof(PatientID));
                if (!result.Success) return result;

                result = Validations.IsInt(PatientID);
                if (!result.Success) return result;

                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.AppointmentExists(PatientID, AppointmentDate, (patientId, appointmentDate) =>
                {
                    return _context.Appointments.Any(a => a.PatientID == patientId && a.AppointmentDate == appointmentDate);
                });
                if (!result.Success) return result;

                result = Validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

                result = await Validations.PatientExists(PatientID, async (id) =>
                {
                    return await _context.Appointments.AnyAsync(p => p.Id == id);
                });
                if (!result.Success) return result;

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
       
        public async Task<OperationResult> GetAppointmentsByPatientAsync(int PatientID)
        {
            OperationResult result = new OperationResult();
            
            try
            {
                result = Validations.IsNullOrWhiteSpace(PatientID, nameof(PatientID));
                if (!result.Success) return result;

                result = Validations.IsInt(PatientID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.PatientID == PatientID).ToListAsync();
                result.Success = true;
                result.Message = "Cita encontrada con exito";
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
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;
               
                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID).ToListAsync();
                result.Success = true;
                result.Message = "Cita encontrada con exito";
            }
            catch (Exception ex) 
            {
                result.Message = _configuration["AppointmentsRepositoryError: GetAppointmentsByDoctorAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetAppointmentsByStatusAsync(int StatusID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(StatusID, nameof(StatusID));
                if (!result.Success) return result;

                result = Validations.IsInt(StatusID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.StatusID == StatusID).ToListAsync();

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
            try
            {
                result = Validations.IsNullOrWhiteSpace(AppointmentDate, nameof(AppointmentDate));
                if (!result.Success) return result;

                result = Validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

               var appointments = await _context.Appointments.Where(a => a.AppointmentDate == AppointmentDate).ToListAsync();

                result.Success = true;
                result.Message = "Lista de todas las citas en esta fecha";
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
            try
            {
                result = Validations.IsNullOrWhiteSpace(AppointmentID, nameof(AppointmentID));
                if (!result.Success) return result;

                result = Validations.IsInt(AppointmentID);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.FindAsync(AppointmentID);
                appointments.StatusID = 2;
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Cita cancelada con exito.";
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
            
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.CheckDate(AppointmentDate);
                if (!result.Success) return result;

                var appointments = await _context.Appointments.Where(a => a.DoctorID == DoctorID && a.AppointmentDate == AppointmentDate).ToListAsync();
                result.Success = true;
                result.Message = "Cita encontrada exictosamente";
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
