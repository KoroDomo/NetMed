using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<StatusRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly JsonMessage _jsonMessage;

        public StatusRepository(NetmedContext context,
                               ILogger<StatusRepository> logger,
                               JsonMessage jsonMessage) : base(context, logger, jsonMessage)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = jsonMessage;
        }

        public override Task<List<Status>> GetAllAsync()
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync();
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Status, bool>> filter)
        {
            _logger.LogInformation(_jsonMessage.SuccessMessages["GetAllEntity"]);
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetStatusByIdAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, _jsonMessage.ErrorMessages["StatusNotFound"]);

                if (!notNullStatus.Success)
                {
                    _logger.LogWarning(notNullStatus.Message);
                    return notNullStatus;
                }

                _logger.LogInformation(_jsonMessage.SuccessMessages["StatusFound"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["StatusFound"], Data = status };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> CreateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                await _context.statuses.AddAsync(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityCreated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityCreated"], Data = status };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> UpdateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                _context.statuses.Update(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityUpdated"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityUpdated"], Data = status };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> DeleteStatusAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, _jsonMessage.ErrorMessages["StatusNotFound"]);

                if (!notNullStatus.Success)
                {
                    _logger.LogWarning(notNullStatus.Message);
                    return notNullStatus;
                }

                _context.statuses.Remove(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_jsonMessage.SuccessMessages["EntityDeleted"]);
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["EntityDeleted"], Data = status };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }
    }
}