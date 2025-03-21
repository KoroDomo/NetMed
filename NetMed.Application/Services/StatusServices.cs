
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Status;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;

namespace NetMed.Application.Services
{
    public class StatusServices : IStatusContract
    {
        private readonly NetmedContext _context;
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusServices> _logger;
        private readonly JsonMessage _jsonMessageMapper;

        public StatusServices(NetmedContext context, IStatusRepository statusRepository,
                                                     ILogger<StatusServices> logger ,
                                                     JsonMessage jsonMessageMapper)
        {

            _logger = logger;
            _statusRepository = statusRepository;
            _context = context;
            _jsonMessageMapper = jsonMessageMapper;

        }

       
        public async Task<OperationResult> GetAllDto()
        {
            OperationResult result = new OperationResult();

            try
            {
                var statu = await _statusRepository.GetAllAsync();
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["StatusFound"]);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.ErrorMessages["StatusFound"] };
            }


            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> GetDtoById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {

                if (!result.Success)
                {
                    return result;
                }

                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["StatusFound"]);
                return new OperationResult { Success = true, Message =_jsonMessageMapper.SuccessMessages["StatusFound"]};

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };

            }

        }

        public async Task<OperationResult> SaveDto(SaveStatusDto dtoSave)
        {
            OperationResult result = new OperationResult();
            try
            {
                var statu = new Status
                {
                    
                    StatusName = dtoSave.StatusName,

                };

                var roles = await _statusRepository.CreateStatusAsync(statu);
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["StatusCreated"]);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["StatusCreated"], Data = dtoSave };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

        public async Task<OperationResult> UpdateDto(UpdateStatusDto dtoUpdate)
        {
            OperationResult result = new OperationResult();
            try
            {
                var statu = new Status
                {
                    Id = dtoUpdate.StatusID,
                    StatusName = dtoUpdate.StatusName,

                };

                var roles = await _statusRepository.UpdateStatusAsync(statu);
                return new OperationResult { Success = true, Message = "Status actualizado con éxito" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }


        }

        public async Task<OperationResult> DeleteDto(int dtoDelete)
        {
            try
            {
                var rolDeleted = await _statusRepository.DeleteStatusAsync(dtoDelete);
                _logger.LogInformation(_jsonMessageMapper.SuccessMessages["StatusDeleted"]);
                return new OperationResult { Success = true, Message = _jsonMessageMapper.SuccessMessages["StatusDeleted"], Data = dtoDelete };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _jsonMessageMapper.ErrorMessages["DatabaseError"]);
                return new OperationResult { Success = false, Message = _jsonMessageMapper.ErrorMessages["DatabaseError"] };
            }
        }

        public Task<OperationResult> GetDtoById(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteDto(DeleteStatusDto dtoDelete)
        {
            throw new NotImplementedException();
        }
    }
}
