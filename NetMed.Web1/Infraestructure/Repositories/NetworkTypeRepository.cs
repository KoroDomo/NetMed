using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.NetworkType;
using NetMed.ApiConsummer.Infraestructure.Base;
using NetMed.ApiConsummer.Infraestructure.Validator.Implementacions;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Persistence.Repositories
{
    public class NetworkTypeRepository : BaseRepository, INetworkTypeRepository
    {
        
        private readonly ICustomLogger _logger;
        private readonly IOperationValidator _validator;
        private readonly IMessageService _messageService;
        public NetworkTypeRepository(HttpClient httpClient, ICustomLogger logger) : base(httpClient)
        {
            _logger = logger;
            BaseEndpoint = "NetworkTypeApi";
            _validator = new OperationValidator(_logger);
            _messageService = new MessageService();
        }
        public async Task<OperationResult<SaveNetworkTypeModel>> CreateNetworkAsync(SaveNetworkTypeModel network)
        {
            const string operation = "Save";
            var result = _validator.CheckNull(network, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return await CreateAsync("SaveNetworkType", network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }

        }
        public async Task<OperationResult<RemoveNetworkTypeModel>> DeleteNetworkAsync(RemoveNetworkTypeModel network)
        {
            const string operation = "Delete";
            var result = _validator.CheckNull(network, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return await DeleteAsync("RemoveNetworkType", network);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<TEntity>> GetNetworkByIdAsync<TEntity>(int id)
        {
            const string operation = "Get";
            var result = _validator.CheckId<TEntity>(id, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                result = await GetAsync<TEntity>("GetNetworkTypeBy", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ListOperationResult<GetNetworkTypeModel>> GetAllNetworksAsync()
        {
            const string operation = "Get";
            var result = new ListOperationResult<GetNetworkTypeModel>();
            try
            {
                result = await GetAllAsync<GetNetworkTypeModel>("GetNetworkTypes");
                if (result == null && result.success == false)
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
                result.Message = $"{_messageService.GetErrorMessage(operation)}";
                _logger.LogError(ex, result.Message);
                return result;

            }
        }

        public async Task<OperationResult<UpdateNetworkTypeModel>> UpdateNetworkAsync(UpdateNetworkTypeModel network)
        {
            const string operation = "Update";
            var result = _validator.CheckNull(network, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                result = await UpdateAsync("UpdateNetworkType", network);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
