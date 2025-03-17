using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.IValidators;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;


namespace NetMed.Persistence.Repositories
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        private readonly NetMedContext _context;
        private readonly ILoggerSystem _logger;
        private readonly IValidations _validations;
        private readonly IMessageService _messageService;

        public DoctorAvailabilityRepository(NetMedContext context, ILoggerSystem logger, IMessageService messageService, IValidations validations) : base(context)
        {
            _context = context;
            _logger = logger;
            _messageService = messageService;
            _validations = validations;
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
                result.Message = "AppointmentsRepositoryError: GetAllAsync";
                result.Success = false;
                
            }
            return result;
        }
        public override async Task<OperationResult> SaveEntityAsync(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.Success) return result;

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Datos Guardados con exito" ;
            }

            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: SaveEntityAsync";
                result.Success = false;
        
            }
            return result;
            
        }
        public async override Task<OperationResult> UpdateEntityAsync(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.Success) return result;

                await base.UpdateEntityAsync(entity);
                result.Success = true;
                result.Message = "Datos Actualizados con exito";
            }
            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: UpdateEntityAsync";
                result.Success = false;
               
            }
            return result;
        }
        public async override Task<OperationResult> ExistsAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(filter);
                if (!result.Success) return result;
                await base.ExistsAsync(filter);
            }
            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: ExistsAsync";
                result.Success = false;
                
            }
            return result; ;
        }
        public async override Task<OperationResult> GetAllAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(filter);
                if (!result.Success) return result;

                await base.GetAllAsync(filter);
            }
            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: GetAllAsync";
                result.Success = false;
                
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

                await base.GetEntityByIdAsync(Id);
                result.Success = true;
                result.Message = "Datos Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: GetEntityByIdAsync";
                result.Success = false;
                
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
                    return await _context.DoctorAvailability.AnyAsync(a => a.Id == id);
                });
                if (!result.Success) return result;

                await base.RemoveAsync(Id);
                result.Success = true;
                result.Message = "Datos desactivados con exito";
            }
            catch (Exception ex)
            {
                result.Message = "DoctorAvailabilityRepositoryError: RemoveAsync";
                result.Success = false;
                
            }
            return result;
        }
        public async Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = _validations.Time(StartTime, EndTime);
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
                result.Message ="ErrorDoctorAvailabilityRepository: SetAvailabilityAsync";
                result.Success = false;
                
            }
            return result;
        }
        public async Task<OperationResult> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;
       
                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability
                    .Where(a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                result.Message = "ErrorDoctorAvailabilityRepository: GetAvailabilityByDoctorAndDateAsync";
                result.Success = false;
                
            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityAsync(int AvailabilityID, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(AvailabilityID);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(AvailabilityID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = _validations.Time(StartTime, EndTime);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);

                result.Success = true;
                result.Message = "Disponibilidad actulizada exitosamente.";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "ErrorDoctorAvailabilityRepository: UpdateAvailabilityAsync";
                result.Success = false;
                
            }
            return result;
        }
        public async Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(AvailabilityID);
                if (!result.Success) return result;

                result = _validations.IsNullOrWhiteSpace(AvailabilityID);
                if (!result.Success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);
                _context.DoctorAvailability.Remove(availability);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Disponibilidad desactivada con exito";
            }
            catch (Exception ex)
            {
                result.Message = "ErrorDoctorAvailabilityRepository: RemoveAvailabilityAsync";
                result.Success = false;
                
            }
            return result;
        }
        public async Task<OperationResult> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
           
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = _validations.Time(StartTime, EndTime);
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
                result.Message ="ErrorDoctorAvailabilityRepository: IsDoctorAvailableAsync";
                result.Success = false;
               

            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.Success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.Success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.Success) return result;

                result = _validations.Time(StartTime, EndTime);
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
                result.Message = "ErrorDoctorAvailabilityRepository: UpdateAvailabilityInRealTimeAsyn";
                result.Success = false;
               
            }
            return result;
        }
    }
}
    


