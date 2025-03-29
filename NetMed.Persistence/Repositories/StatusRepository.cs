using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NetmedContext _context;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessage;
        private readonly IStatusValidator _startupValidator;
        public StatusRepository(NetmedContext context,
                              ILoggerCustom logger,
                               JsonMessage jsonMessage) : base(context)
        {
            _context = context;
            _logger = logger;
            _jsonMessage = jsonMessage;
            _startupValidator = new StatusValidator(logger, jsonMessage);
        }

        public override Task<OperationResult> GetAllAsync()
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
            var validationResult = _startupValidator.ValidateIsStatusIdNotIsNegative(statusId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = _startupValidator.ValidateStatusIsNotNull(status, _jsonMessage.ErrorMessages["StatusNotFound"]);

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

            var validationResult = _startupValidator.ValidateStatusIsNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            var validationLessZero = _startupValidator.ValidateIsStatusIdNotIsNegative(status.Id, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationLessZero.Success)
            {
                _logger.LogError(validationLessZero.Message);
                return validationLessZero;
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
            var validationResult = _startupValidator.ValidateStatusIsNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            var validationLessZero = _startupValidator.ValidateIsStatusIdNotIsNegative(status.Id, _jsonMessage.ErrorMessages["InvalidId"]);


            if (!validationLessZero.Success)
            {
                _logger.LogError(validationLessZero.Message);
                return validationLessZero;
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

            var validationResult = _startupValidator.ValidateIsStatusIdNotIsNegative(statusId, _jsonMessage.ErrorMessages["InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogError(validationResult.Message);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = _startupValidator.ValidateIsEntityIsNull(status, _jsonMessage.ErrorMessages["StatusNotFound"]);
                

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