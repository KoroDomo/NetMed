using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Interfaces;
using System.Data;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;



namespace NetMed.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NetmedContext _context;
        private readonly ILogger<StatusRepository> _logger;
        private readonly IConfiguration _configuration;

        public StatusRepository(NetmedContext context,
                               ILogger<StatusRepository> logger,
                               IConfiguration configuration) : base(context,logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override Task<Status> GetEntityByIdAsync(int statusID)
        {
            return base.GetEntityByIdAsync(statusID);
        }

        public override Task<OperationResult> SaveEntityAsync(Status entity)
        {
            return base.SaveEntityAsync(entity);
        }

        public override Task<List<Status>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override Task<OperationResult> UpdateEntityAsync(Status entity)
        {
            return base.UpdateEntityAsync(entity);
        }

        public override Task<OperationResult> GetAllAsync(Expression<Func<Status, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public async Task<OperationResult> GetStatusByIdAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "statusId", statusId);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, _configuration["ErrorMessages:StatusNotFound"]);

                if (!notNullStatus.Success)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:StatusNotFound"], "statusId", statusId);
                    return notNullStatus;
                }

                _logger.LogInformation(_configuration["ErrorMessages:StatusRetrieved"], "statusId", statusId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:StatusRetrieved"], Data = status };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> CreateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "status", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.statuses.Add(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:StatusCreated"], "statusId", status.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:StatusCreated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> UpdateStatusAsync(Status status)
        {
            var validationResult = EntityValidator.ValidateNotNull(status, _configuration["ErrorMessages:NullEntity"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "status", "Entidad nula");
                return validationResult;
            }

            try
            {
                _context.statuses.Update(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:StatusUpdated"], "statusId", status.Id);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:StatusUpdated"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }

        public async Task<OperationResult> DeleteStatusAsync(int statusId)
        {
            var validationResult = EntityValidator.ValidatePositiveNumber(statusId, _configuration["ErrorMessages:InvalidId"]);

            if (!validationResult.Success)
            {
                _logger.LogWarning(_configuration["ErrorMessages:ValidationFailed"], "statusId", statusId);
                return validationResult;
            }

            try
            {
                var status = await _context.statuses.FindAsync(statusId);

                var notNullStatus = EntityValidator.ValidateNotNull(status, _configuration["ErrorMessages:StatusNotFound"]);

                if (!notNullStatus.Success)
                {
                    _logger.LogWarning(_configuration["ErrorMessages:StatusNotFound"], "statusId", statusId);
                    return notNullStatus;
                }

                _context.statuses.Remove(status);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_configuration["ErrorMessages:StatusDeleted"], "statusId", statusId);
                return new OperationResult { Success = true, Mesagge = _configuration["ErrorMessages:StatusDeleted"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["ErrorMessages:DatabaseError"], ex.Message);
                return new OperationResult { Success = false, Mesagge = _configuration["ErrorMessages:GeneralError"] };
            }
        }
    }
}