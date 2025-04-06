using NetMed.ApiConsummer.Application.Contracts;
using NetMed.ApiConsummer.Core.Base;
using NetMed.ApiConsummer.Core.Models.InsuranceProvider;
using NetMed.ApiConsummer.Persistence.Interfaces;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {

        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ICustomLogger _logger;

        public InsuranceProviderService(IInsuranceProviderRepository repository,
                                        ICustomLogger logger)
        {
            _insuranceProviderRepository = repository;
            _logger = logger;

        }

        public async Task<OperationResult> GetAll()
        {
            var repositoryResult = new OperationResult();
            try
            {
                repositoryResult = await _insuranceProviderRepository.GetAllProvidersAsync();

                if (repositoryResult.Success == false) return repositoryResult;

                return repositoryResult;

                //return _operations.SuccessResult(dtos, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                //    _logger.LogError(ex, _operations.GetErrorMessage("Operations", "GetFailed"));
                //    return _operations.HandleException("Operations", "GetFailed");
                return repositoryResult;

            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();
            try
            {
                result = await _insuranceProviderRepository.GetProviderByIdAsync<GetInsuranceProviderModel>(id);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
                
            }
        }

        public async Task<OperationResult> Remove(RemoveInsuranceProviderModel dto)
        {
            var result = new OperationResult();
            try
            {
                result = await _insuranceProviderRepository.DeleteProviderAsync(dto);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
        }

        public async Task<OperationResult> Save(SaveInsuranceProviderModel dto)
        {
            var result = new OperationResult();
            try
            {
                result = await _insuranceProviderRepository.CreateProviderAsync(dto);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
        }

        public async Task<OperationResult> Update(UpdateInsuranceProviderModel dto)
        {
            var result = new OperationResult();
            try
            {
                result = await _insuranceProviderRepository.UpdateProviderAsync(dto);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;

            }
        }
    }
}
