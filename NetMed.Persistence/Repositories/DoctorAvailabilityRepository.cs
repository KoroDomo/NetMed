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
        public async Task<List<DoctorAvailability>> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate)
        {
            OperationResult result = new OperationResult();

            try
            {
                return await _context.DoctorAvailabilities.Where(a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate).ToListAsync();    
            }
            catch (Exception ex)
            {
                result.Message = _configuration["ErrorDoctorAvailabilityRepository: GetAvailabilityByDoctorAndDateAsync"];
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
                return new List<DoctorAvailability>();
            }          
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
                var querys = await (from DoctorAvailability in _context.DoctorAvailabilities
                             join appts in _context.Appointments on DoctorAvailability.DoctorID equals         appts.Id
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
                _logger.LogError(result.Message,ex.ToString());
            }
            return result;
        }

        public Task<List<DoctorAvailability>> GetFutureAvailabilityByDoctorAsync(int DoctorID)
        {
            throw new NotImplementedException();
        }

        public Task<List<DoctorAvailability>> GetGeneralAvailabilityByDoctorAsync(int DoctorID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateAvailabilityAsync(int availabilityId, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime, bool isAvailable)
        {
            throw new NotImplementedException();
        }
    } 
}

