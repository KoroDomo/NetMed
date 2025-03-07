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
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<DoctorAvailabilityRepository> _logger;
        private readonly IConfiguration _configuration;

        public DoctorAvailabilityRepository(NetMedContext context, ILogger<DoctorAvailabilityRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async override Task<OperationResult> GetAllAsync()
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
        public override async Task<OperationResult> SaveEntityAsync(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(entity, nameof(entity));
                if (!result.Success) return result;

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Datos Guardados con exito" ;
            }

            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityRepositoryError: SaveEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
            
        }
        public async override Task<OperationResult> UpdateEntityAsync(DoctorAvailability entity)
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
                result.Message = _configuration["DoctorAvailabilityRepositoryError: UpdateEntityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async override Task<OperationResult> ExistsAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(filter, nameof(filter));
                if (!result.Success) return result;
                await base.ExistsAsync(filter);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityRepositoryError: ExistsAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result; ;
        }
        public async override Task<OperationResult> GetAllAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(filter, nameof(filter));
                if (!result.Success) return result;

                await base.GetAllAsync(filter);
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityRepositoryError: GetAllAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
           return result;
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

                await base.GetEntityByIdAsync(Id);
                result.Success = true;
                result.Message = "Datos Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityRepositoryError: GetEntityByIdAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> RemoveAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsInt(Id);
                if (!result.Success) return result;

                result = Validations.IsNullOrWhiteSpace(Id, nameof(Id));
                if (!result.Success) return result;

                result = await Validations.ExistsEntity(Id, async (id) =>
                {
                    return await _context.DoctorAvailability.AnyAsync(a => a.Id == id);
                });
                if (!result.Success) return result;

                await base.RemoveAsync(Id);
                result.Success = true;
                result.Message = "Datos desactivados con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["DoctorAvailabilityRepositoryError: RemoveAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = Validations.Time(StartTime, EndTime);
                if (!result.Success) return result;

                var newAvailability = new DoctorAvailability
                {
                    DoctorID = DoctorID,
                    AvailableDate = AvailableDate,
                    StartTime = StartTime,
                    EndTime = EndTime
                };
                _context.DoctorAvailability.Add(newAvailability);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Disponibilidad establecidad correctamente.";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: SetAvailabilityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;
       
                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability
                    .Where(a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: GetAvailabilityByDoctorAndDateAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityAsync(int AvailabilityID, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(AvailabilityID);
                if (!result.Success) return result;

                result = Validations.IsNullOrWhiteSpace(AvailabilityID, nameof(AvailabilityID));
                if (!result.Success) return result;

                result = Validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = Validations.Time(StartTime, EndTime);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);

                result.Success = true;
                result.Message = "Disponibilidad actulizada exitosamente.";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: UpdateAvailabilityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsInt(AvailabilityID);
                if (!result.Success) return result;

                result = Validations.IsNullOrWhiteSpace(AvailabilityID, nameof(AvailabilityID));
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);
                _context.DoctorAvailability.Remove(availability);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Disponibilidad desactivada con exito";
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: RemoveAvailabilityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public async Task<OperationResult> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
           
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = Validations.Time(StartTime, EndTime);
                if (!result.Success) return result;

                bool overlappingAvailability = await _context.DoctorAvailability.AnyAsync
                    (a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate &&
                    (a.StartTime < EndTime && a.EndTime > StartTime)); 

                if (!overlappingAvailability)
                {
                    result.Success = true;
                    result.Message = "El doctor está disponible.";
                    return result;
                }
                 result.Success = false;
                 result.Message = "El doctor no está disponible para este horario.";
                
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: IsDoctorAvailableAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());

            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = Validations.IsNullOrWhiteSpace(DoctorID, nameof(DoctorID));
                if (!result.Success) return result;

                result = Validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = Validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = Validations.Time(StartTime, EndTime);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability.FirstOrDefaultAsync(a =>
                   a.DoctorID == DoctorID &&
                   a.AvailableDate == AvailableDate &&
                   a.StartTime == StartTime &&
                   a.EndTime == EndTime);

                if (availability == null)
                {
                    availability = new DoctorAvailability
                    {
                        DoctorID = DoctorID,
                        AvailableDate = AvailableDate,
                        StartTime = StartTime,
                        EndTime = EndTime
                    };
                    _context.DoctorAvailability.Add(availability);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Estas intentado agregar datos ya existentes";
                }
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Cambios actualizados con exito" ;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: UpdateAvailabilityInRealTimeAsyn"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
    


