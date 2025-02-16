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
        public override Task<List<DoctorAvailability>> GetAllAsync()
        {
            return base.GetAllAsync();
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
                Task.FromResult(false);
            }
            return await base.GetEntityByIdAsync(id);
        }

        public async Task<OperationResult> GetDoctorAvailabilityByAppointments(int AppointmentID)
        {
            OperationResult result = new OperationResult();

            var validationResult = EntityValidator.Validator(AppointmentID, nameof(AppointmentID));
            if (!validationResult.Success)
            {
                return validationResult;
            }
            try
            {
                var querys = await(from DoctorAvailability in _context.DoctorAvailabilities
                                   join appts in _context.Appointments on DoctorAvailability.DoctorID equals appts.Id
                                   where DoctorAvailability.DoctorID == AppointmentID
                                   select new DoctorAvailabilityModel()
                                   {
                                       AvailabilityID = DoctorAvailability.Id,
                                       DoctorID = DoctorAvailability.DoctorID,
                                       AvailableDate = DoctorAvailability.AvailableDate,
                                       StarTime = DoctorAvailability.StartTime,
                                       EndTime = DoctorAvailability.EndTime,
                                   }).ToListAsync();
                result.Data = querys;
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: GetDoctorAvailabilityByAppointments"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;

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
        public Task<DoctorAvailability> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateAvailabilityAsync(int availabilityId, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime, bool isAvailable)
        {
            throw new NotImplementedException();
        }
    } 
}

