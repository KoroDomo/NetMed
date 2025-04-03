using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Status;
using NetMed.Application.Interfaces;
using NetMed.Application.Mapper;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validator.Implementations;
using NetMed.Infraestructure.Validator.Interfaz;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Services
{
    public class StatusService : IStatusContract
    {
        private readonly NetmedContext _context;
        private readonly IStatusRepository _statusRepository;
        private readonly ILoggerCustom _logger;
        private readonly JsonMessage _jsonMessage;
        private readonly IStatusValidator _statusValidator;

        public StatusService(NetmedContext context,
                             IStatusRepository statusRepository,
                             ILoggerCustom logger,
                             JsonMessage jsonMessage)
        {
            _context = context;
            _statusRepository = statusRepository;
            _logger = logger;
            _jsonMessage = jsonMessage;
            _statusValidator = new StatusValidator(logger, jsonMessage);
        }

        public async Task<OperationResult> GetAllDto()
        {
            var result = new OperationResult();
            try
            {
                var repositoryResult = await _statusRepository.GetAllAsync();
                if (!repositoryResult.Success || repositoryResult.Data == null)
                {
                    _logger.LogInformation(_jsonMessage.ErrorMessages["GetAllEntity"], "Status");
                    return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["GetAllEntity"] };

                }

                var statuses = (IEnumerable<Status>)repositoryResult.Data;
                var statusDtos = StatusMapper.ToDtoList(statuses);

                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["GetAllEntity"], Data = statusDtos };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> GetDtoById(int statusId)
        {
          
            var result = new OperationResult();
            try
            {
                var status = await _statusRepository.GetStatusByIdAsync(statusId);
                var statusDto = StatusMapper.ToDto(status.Data);
                return result.Data = status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }

        }
        
        public async Task<OperationResult> SaveDto(SaveStatusDto dtoSave)
        {
            var result = new OperationResult();
            try
            {
                var status = new Status
                {
                    StatusName = dtoSave.StatusName
                };

                var validationResult = _statusValidator.ValidateStatusIsNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _statusRepository.CreateStatusAsync(status);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var savedStatus = (Status)repositoryResult.Data;
                var statusDto = StatusMapper.ToDto(savedStatus);

                _logger.LogInformation(_jsonMessage.SuccessMessages["StatusCreated"], nameof(Status), savedStatus.Id);
                return new OperationResult{Success = true, Message = _jsonMessage.SuccessMessages["StatusCreated"], Data = statusDto };
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
           
        }

        public async Task<OperationResult> UpdateDto(UpdateStatusDto dtoUpdate)
        {
            var result = new OperationResult();
            try
            {
                var validationResult = _statusValidator.ValidateIsStatusIdNotIsNegative(dtoUpdate.id, _jsonMessage.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var status = new Status
                {
                    Id = dtoUpdate.id,
                    StatusName = dtoUpdate.StatusName
                };

                var nullValidationResult = _statusValidator.ValidateStatusIsNotNull(status, _jsonMessage.ErrorMessages["NullEntity"]);
                if (!nullValidationResult.Success)
                {
                    _logger.LogError(nullValidationResult.Message);
                    return nullValidationResult;
                }

                var repositoryResult = await _statusRepository.UpdateStatusAsync(status);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }

                var updatedStatus = (Status)repositoryResult.Data;
                var statusDto = StatusMapper.ToDto(updatedStatus);

                _logger.LogInformation(_jsonMessage.SuccessMessages["StatusUpdated"], nameof(Status), updatedStatus.Id);
              
                return new OperationResult { Success = true, Message = _jsonMessage.SuccessMessages["StatusUpdated"], Data = statusDto };

            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessage.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> DeleteDto(int statusId)
        {
            var result = new OperationResult();
            try
            {

                var validationResult = _statusValidator.ValidateNumberEntityIsNegative(statusId, _jsonMessage.ErrorMessages["InvalidId"]);
                if (!validationResult.Success)
                {
                    _logger.LogError(validationResult.Message);
                    return validationResult;
                }

                var repositoryResult = await _statusRepository.DeleteStatusAsync(statusId);
                if (!repositoryResult.Success)
                {
                    return repositoryResult;
                }


                _logger.LogInformation(_jsonMessage.SuccessMessages["StatusDeleted"], nameof(Status));
                result.Success = true;
                result.Message = _jsonMessage.SuccessMessages["StatusDeleted"];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessage.ErrorMessages["DatabaseError"]);
                result.Success = false;
                result.Message = _jsonMessage.ErrorMessages["DatabaseError"];
            }
            return result;

        }
    }
}