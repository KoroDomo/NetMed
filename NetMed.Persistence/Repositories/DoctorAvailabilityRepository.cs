using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;
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
        public async override Task<List<DoctorAvailability>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }
        public override async Task<OperationResult> SaveEntityAsync(DoctorAvailability entity)
        {
            var validationResult = EntityValidator.Validator(entity, nameof(entity));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.SaveEntityAsync(entity);
        }
        public async override Task<OperationResult> UpdateEntityAsync(DoctorAvailability entity)
        {
            var validationResult = EntityValidator.Validator(entity, nameof(entity));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.UpdateEntityAsync(entity);
        }
        public override Task<bool> ExistsAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            var validationResult = EntityValidator.Validator(filter, nameof(filter));
            if (!validationResult.Success)
            {
                return Task.FromResult(false);
            }
            return base.ExistsAsync(filter);
        }
        public async override Task<OperationResult> GetAllAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            var validationResult = EntityValidator.Validator(filter, nameof(filter));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            return await base.GetAllAsync(filter);
        }
        public async override Task<DoctorAvailability> GetEntityByIdAsync(int id)
        {

            var validationResult = EntityValidator.Validator(id, nameof(id));
            if (!validationResult.Success)
            {
                await Task.FromResult(false);
            }
            return await base.GetEntityByIdAsync(id);
        }
        public async Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            var doctorIDResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIDResult.Success) return doctorIDResult;

            if (AvailableDate < DateOnly.FromDateTime(DateTime.Now))
            {
                return new OperationResult { Success = false, Message = "La fecha disponible debe ser futura." };
            }
            if (StartTime >= EndTime)
            {
                return new OperationResult { Success = false, Message = "La hora de inicio debe ser anterior a la hora de finalización." };
            }
            try
            {
                var newAvailability = new DoctorAvailability
                {
                    DoctorID = DoctorID,
                    AvailableDate = AvailableDate,
                    StartTime = StartTime,
                    EndTime = EndTime
                };
                _context.DoctorAvailabilities.Add(newAvailability);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Disponibilidad establecidad correctamente." };
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
            var doctorIDResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIDResult.Success) return doctorIDResult;

            if (AvailableDate > DateOnly.FromDateTime(DateTime.Now))
            {
                result.Success = false;
                result.Message = "La fecha debe ser mayor a la fecha actual";
                return result;
            }
            try
            {
                var availability = await _context.DoctorAvailabilities
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

            var availabilityIdResult = EntityValidator.Validator(AvailabilityID, nameof(AvailabilityID));
            if (!availabilityIdResult.Success) return availabilityIdResult;

            var doctorIDResult = EntityValidator.Validator(DoctorID, nameof(DoctorID));
            if (!doctorIDResult.Success) return doctorIDResult;

            if (AvailableDate > DateOnly.FromDateTime(DateTime.Now))
            {
                return new OperationResult { Success = false, Message = "La fecha de disponibilidad para actualizar debe ser futura." };
            }
            if (StartTime >= EndTime)
            {
                return new OperationResult { Success = false, Message = "La hora de inicio debe ser anterior a la hora de finalización." };
            }
            if (await Existingavailability(AvailabilityID, DoctorID, AvailableDate, StartTime, EndTime))
            {
                return new OperationResult { Success = false, Message = "El rango de tiempo seleccionado se superpone con una disponibilidad existente." };
            }
            try
            {
                var availability = await _context.DoctorAvailabilities.FindAsync(AvailabilityID);

                if (availability == null)
                {
                    return new OperationResult { Success = false, Message = "Disponibilidad no encontrada." };
                }

                //Actualizacion solo si los campos son diferentes.
                if (availability.DoctorID != DoctorID) availability.DoctorID = DoctorID;
                if (availability.AvailableDate != AvailableDate) availability.AvailableDate = AvailableDate;
                if (availability.StartTime != StartTime) availability.StartTime = StartTime;
                if (availability.EndTime != EndTime) availability.EndTime = EndTime;
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Disponibilidad actulizada exitosamente." };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: UpdateAvailabilityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        //Metodo para verificar si los datos existen antes de actualizar
        private async Task<bool> Existingavailability(int AvailabilityID, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            return await _context.DoctorAvailabilities.AnyAsync(a =>
                a.Id != AvailabilityID &&
                a.DoctorID == DoctorID &&
                a.AvailableDate == AvailableDate &&
                a.StartTime < EndTime &&
                a.EndTime > StartTime);
        }
        public async Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            OperationResult result = new OperationResult();
            var availabilityIdResult = EntityValidator.Validator(AvailabilityID, nameof(AvailabilityID));
            if (!availabilityIdResult.Success) return availabilityIdResult;

            try
            {
                var availability = await _context.DoctorAvailabilities.FindAsync(AvailabilityID);

                if (availability == null)
                {
                    return new OperationResult { Success = false, Message = "Disponibilidad no encontrada" };
                }
                _context.DoctorAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Disponibilidad eliminada con exito" };
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: RemoveAvailabilityAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;

        }
        public async Task<bool> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();

            try
            {
                var overlappingAvailability = await _context.DoctorAvailabilities.AnyAsync(a =>
                    a.DoctorID == DoctorID &&
                    a.AvailableDate == AvailableDate &&
                    a.StartTime < EndTime &&
                    a.EndTime > StartTime);

                return !overlappingAvailability;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: IsDoctorAvailableAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());

            }
            return false;
        }
        public async Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();

            try
            {
                var availability = await _context.DoctorAvailabilities.FirstOrDefaultAsync(a =>
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
                    _context.DoctorAvailabilities.Add(availability);
                }
                else
                {
                    return new OperationResult { Success = false, Message = "Estas intentado agregar datos ya existentes" };
                }
                await _context.SaveChangesAsync();

                return new OperationResult { Success = true, Message = "Cambios actualizados con exito" };
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
    


