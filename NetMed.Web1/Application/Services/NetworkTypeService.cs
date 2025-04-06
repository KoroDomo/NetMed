using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.NetworkType;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Services
{
    public class NetworkTypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _repository;
        private readonly ICustomLogger _logger;
        private readonly IMessageService _messageService;

        public NetworkTypeService(
            INetworkTypeRepository repository,
            ICustomLogger logger,
            IMessageService messageService)
        {
            _repository = repository;
            _logger = logger;
            _messageService = messageService;
        }

        public async Task<ListOperationResult<GetNetworkTypeModel>> GetAll()
        {
            const string operation = "Get";
            var result = new ListOperationResult<GetNetworkTypeModel>();

            try
            {
                result = await _repository.GetAllNetworksAsync();

                if (result.success== false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _messageService.GetServiceErrorMessage(operation);
                _logger.LogError(ex, $"{result.Message}: {ex.Message}");
                return result;
            }
        }

        public async Task<OperationResult<TEntity>> GetById<TEntity>(int id)
        {
            const string operation = "Get";
            var result = new OperationResult<TEntity>();

            try
            {
                result = await _repository.GetNetworkByIdAsync<TEntity>(id);

                if (result.Success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _messageService.GetServiceErrorMessage(operation);
                _logger.LogError(ex, $"{result.Message}: {ex.Message}");
                return result;
            }
        }

        public async Task<OperationResult<RemoveNetworkTypeModel>> Remove(RemoveNetworkTypeModel dto)
        {
            const string operation = "Delete";
            var result = new OperationResult<RemoveNetworkTypeModel>();
            if (dto.ChangeDate == null) dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _repository.DeleteNetworkAsync(dto);

                if (result.Success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _messageService.GetServiceErrorMessage(operation);
                _logger.LogError(ex, $"{result.Message}: {ex.Message}");
                return result;
            }
        }

        public async Task<OperationResult<SaveNetworkTypeModel>> Save(SaveNetworkTypeModel dto)
        {
            const string operation = "Save";
            var result = new OperationResult<SaveNetworkTypeModel>();
            if (dto.ChangeDate == null) dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _repository.CreateNetworkAsync(dto);

                if (result.Success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _messageService.GetServiceErrorMessage(operation);
                _logger.LogError(ex, $"{result.Message}: {ex.Message}");
                return result;
            }
        }

        public async Task<OperationResult<UpdateNetworkTypeModel>> Update(UpdateNetworkTypeModel dto)
        {
            const string operation = "Update";
            var result = new OperationResult<UpdateNetworkTypeModel>();
            if (dto.ChangeDate == null) dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _repository.UpdateNetworkAsync(dto);

                if (result.Success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _messageService.GetServiceErrorMessage(operation);
                _logger.LogError(ex, $"{result.Message}: {ex.Message}");
                return result;
            }
        }
    }
}