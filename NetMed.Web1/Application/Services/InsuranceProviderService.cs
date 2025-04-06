using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;
using NetMed.ApiConsummer.Infraestructure.Validator.Implementacions;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ICustomLogger _logger;
        private readonly IMessageService _messageService;

        public InsuranceProviderService(
            IInsuranceProviderRepository repository,
            ICustomLogger logger)
        {
            _insuranceProviderRepository = repository;
            _logger = logger;
            _messageService = new MessageService();
        }

        public async Task<ListOperationResult<GetInsuranceProviderModel>> GetAll()
        {
            const string operation = "Get";
            var result = new ListOperationResult<GetInsuranceProviderModel>();

            try
            {
                var repositoryResult = await _insuranceProviderRepository.GetAllProvidersAsync();

                if (repositoryResult.success == false)
                {
                    result.Message = _messageService.GetErrorMessage(operation);
                    _logger.LogWarning(result.Message);
                    return result;
                }

                _logger.LogInformation(_messageService.GetSuccessMessage(operation));
                return repositoryResult;
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
                result = await _insuranceProviderRepository.GetProviderByIdAsync<TEntity>(id);

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

        public async Task<OperationResult<RemoveInsuranceProviderModel>> Remove(RemoveInsuranceProviderModel dto)
        {
            const string operation = "Delete";
            var result = new OperationResult<RemoveInsuranceProviderModel>();
            if(dto.ChangeDate == null)dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _insuranceProviderRepository.DeleteProviderAsync(dto);

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

        public async Task<OperationResult<SaveInsuranceProviderModel>> Save(SaveInsuranceProviderModel dto)
        {
            const string operation = "Save";
            var result = new OperationResult<SaveInsuranceProviderModel>();
            if (dto.ChangeDate == null) dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _insuranceProviderRepository.CreateProviderAsync(dto);

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

        public async Task<OperationResult<UpdateInsuranceProviderModel>> Update(UpdateInsuranceProviderModel dto)
        {
            const string operation = "Update";
            var result = new OperationResult<UpdateInsuranceProviderModel>();
            if (dto.ChangeDate == null) dto.ChangeDate = DateTime.Now;
            try
            {
                result = await _insuranceProviderRepository.UpdateProviderAsync(dto);

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
