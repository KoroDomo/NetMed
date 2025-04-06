using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;
using NetMed.ApiConsummer.Infraestructure.Base;
using NetMed.ApiConsummer.Infraestructure.Validator.Implementacions;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Persistence.Repositories
{
    public class InsuranceProviderRepository : BaseRepository, IInsuranceProviderRepository
    {
        
        private readonly ICustomLogger _logger;
        private readonly IOperationValidator _validator;
        private readonly IMessageService _messageService;

        public InsuranceProviderRepository(HttpClient httpClient, ICustomLogger logger) : base(httpClient)
        {
            _logger = logger;
            BaseEndpoint = "InsuranceProviderApi";
            _validator = new OperationValidator(_logger);
            _messageService = new MessageService();
        }
        public async Task<OperationResult<SaveInsuranceProviderModel>> CreateProviderAsync(SaveInsuranceProviderModel insurance)
        {
            const string operation = "Save";
            var result = _validator.CheckNull(insurance, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return await CreateAsync("SaveInsuranceProvider", insurance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,result.Message);
                return result;
            }

        }
        public async Task<OperationResult<RemoveInsuranceProviderModel>> DeleteProviderAsync(RemoveInsuranceProviderModel insurance)
        {
            const string operation = "Delete";
            var result = _validator.CheckNull(insurance, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return await DeleteAsync("RemoveInsuranceProvider", insurance);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<TEntity>> GetProviderByIdAsync<TEntity>(int id)
        {
            const string operation = "Get";
            var result = _validator.CheckId<TEntity>(id, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return await GetAsync<TEntity>("GetInsuranceProviderBy", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ListOperationResult<GetInsuranceProviderModel>> GetAllProvidersAsync()
        {
            const string operation = "Get";
            var result = new ListOperationResult<GetInsuranceProviderModel>();
            try 
            {
                result = await GetAllAsync<GetInsuranceProviderModel>("GetInsuranceProviders");
                if (result == null && result.success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return result;   
                
            }
            catch(Exception ex)
            {
                result.Message = $"{_messageService.GetErrorMessage(operation)}";
                _logger.LogError(ex, result.Message);
                return result;

            }

        }

        public async Task<OperationResult<UpdateInsuranceProviderModel>> UpdateProviderAsync(UpdateInsuranceProviderModel insurance)
        {
            const string operation = "Update";
            var result = _validator.CheckNull(insurance, operation);
            if (result.Success == false) return result;

            try
            {
                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                result = await UpdateAsync("UpdateInsuranceProvider", insurance);
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
