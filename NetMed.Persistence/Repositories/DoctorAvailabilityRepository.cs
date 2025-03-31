using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
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
        public async override Task<List<DoctorAvailability>> GetAllAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                var datos = await _context.DoctorAvailability.ToListAsync();
                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetAllAsync), true);
                result.data = datos;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex, result.message);   
            }
            return (List<DoctorAvailability>)result.data;
        }
        public override async Task<OperationResult> SaveEntityAsync(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.success) return result;

                result = _validations.CheckDate(entity.AvailableDate);
                if (!result.success) return result;

                var datos = await base.SaveEntityAsync(entity);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(SaveEntityAsync), true);
                result.data = datos;
            }

            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(SaveEntityAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;      
        }
        public async override Task<OperationResult> UpdateEntityAsync(DoctorAvailability entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(entity);
                if (!result.success) return result;

                var datos = await base.UpdateEntityAsync(entity);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(UpdateEntityAsync), true);
                result.data = datos;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(UpdateEntityAsync), false);
                _logger.LogError(ex, result.message);

            }
            return result;
        }
        public async override Task<OperationResult> ExistsAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(filter);
                if (!result.success) return result;

                await base.ExistsAsync(filter);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(ExistsAsync), true);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(ExistsAsync), false);
                _logger.LogError(ex , result.message);  
            }
            return result; ;
        }
        public async override Task<OperationResult> GetAllAsync(Expression<Func<DoctorAvailability, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(filter);
                if (!result.success) return result;

                await base.GetAllAsync(filter);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetAllAsync), true);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex, result.message);
            }
           return result;
        }
        public async override Task<DoctorAvailability> GetEntityByIdAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.success) return result.data;

                result = await _validations.ExistsEntity(Id, async (id) =>
                {
                    return await _context.DoctorAvailability.AnyAsync(a => a.Id == id);
                });
                if (!result.success) return result.data;

                result = _validations.IsInt(Id);
                if (!result.success) return result.data;

                var datos = await _context.DoctorAvailability.Where(a => a.Id == Id).FirstAsync();
                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetEntityByIdAsync), true);
                result.data = datos;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(GetEntityByIdAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result.data;
        }
        public async override Task<OperationResult> RemoveAsync(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(Id);
                if (!result.success) return result;

                result = _validations.IsInt(Id);
                if (!result.success) return result;

                var datos = await base.RemoveAsync(Id);
                result.success = true;
                result.message = _messageService.GetMessage(nameof(RemoveAsync), true);
                result.data = datos;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(RemoveAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.success) return result;

                result = _validations.Time(StartTime, EndTime);
                if (!result.success) return result;

                var newAvailability = new DoctorAvailability
                {
                    DoctorID = DoctorID,
                    AvailableDate = AvailableDate,
                    StartTime = StartTime,
                    EndTime = EndTime
                };
                _context.DoctorAvailability.Add(newAvailability);
                await _context.SaveChangesAsync();
                result.success = true;
                result.message = _messageService.GetMessage(nameof(SetAvailabilityAsync), true);
                result.data = newAvailability;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(SetAvailabilityAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.success) return result;
       
                result = _validations.IsInt(DoctorID);
                if (!result.success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.success) return result;

                var availability = await _context.DoctorAvailability
                    .Where(a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate)
                    .FirstOrDefaultAsync();
                result.success = true;
                result.message = _messageService.GetMessage(nameof(GetAvailabilityByDoctorAndDateAsync), true);
                result.data = availability;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(GetAvailabilityByDoctorAndDateAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityAsync(int AvailabilityID, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(DoctorID);
                if (!result.success) return result;

                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.success) return result;

                result = _validations.IsInt(AvailabilityID);
                if (!result.success) return result;

                result = _validations.IsNullOrWhiteSpace(AvailabilityID);
                if (!result.success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.success) return result;

                result = _validations.Time(StartTime, EndTime);
                if (!result.success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);

                result.success = true;
                result.message = _messageService.GetMessage(nameof(UpdateAvailabilityAsync), true);
                result.data = availability;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(UpdateAvailabilityAsync), false);
                _logger.LogError(ex, result.message); 
            }
            return result;
        }
        public async Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsInt(AvailabilityID);
                if (!result.success) return result;

                result = _validations.IsNullOrWhiteSpace(AvailabilityID);
                if (!result.success) return result;

                var availability = await _context.DoctorAvailability.FindAsync(AvailabilityID);
                _context.DoctorAvailability.Remove(availability);
                await _context.SaveChangesAsync();

                result.success = true;
                result.message = _messageService.GetMessage(nameof(RemoveAvailabilityAsync), true);
                result.data = availability;
            }
            catch (Exception ex)
            {              
                result.success = false;
                result.message = _messageService.GetMessage(nameof(RemoveAvailabilityAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
           
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.success) return result;

                result = _validations.Time(StartTime, EndTime);
                if (!result.success) return result;

                bool overlappingAvailability = await _context.DoctorAvailability.AnyAsync
                    (a => a.DoctorID == DoctorID && a.AvailableDate == AvailableDate &&
                    (a.StartTime < EndTime && a.EndTime > StartTime)); 

                if (!overlappingAvailability)
                {
                    result.success = true;
                    result.message = _messageService.GetMessage(nameof(IsDoctorAvailableAsync), true);
                    return result;
                }
                 result.success = false;
                 result.message = _messageService.GetMessage(nameof(IsDoctorAvailableAsync), false);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(IsDoctorAvailableAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
        public async Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime)
        {
            OperationResult result = new OperationResult();
            try
            {
                result = _validations.IsNullOrWhiteSpace(DoctorID);
                if (!result.success) return result;

                result = _validations.IsInt(DoctorID);
                if (!result.success) return result;

                result = _validations.CheckDate(AvailableDate);
                if (!result.success) return result;

                result = _validations.Time(StartTime, EndTime);
                if (!result.success) return result;

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
                    result.success = false;
                    result.message = _messageService.GetMessage(nameof(UpdateAvailabilityInRealTimeAsync), false);
                }
                await _context.SaveChangesAsync();
                result.success = true;
                result.message = _messageService.GetMessage(nameof(UpdateAvailabilityInRealTimeAsync), true);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = _messageService.GetMessage(nameof(UpdateAvailabilityInRealTimeAsync), false);
                _logger.LogError(ex, result.message);
            }
            return result;
        }
    }
}
    


