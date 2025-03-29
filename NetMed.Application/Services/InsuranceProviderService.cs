using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Application.Contracts;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using NetMed.Domain.Entities;
using AutoMapper;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators.Implementations;
using NetMed.Infraestructure.Validators.Interfaces;

namespace NetMed.Application.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ICustomLogger _logger;
        private readonly IInsuranceProviderValidator _operations;
        private readonly IMapper _mapper;

        public InsuranceProviderService(IInsuranceProviderRepository repository,
                                        ICustomLogger logger,
                                        IMapper mapper)
        {
            _insuranceProviderRepository = repository;
            _logger = logger;
            _operations = new InsuranceProviderValidator();
            _mapper = mapper;
            
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {
                var repositoryResult = await _insuranceProviderRepository.GetAllAsync();

                if (!repositoryResult.Success) return repositoryResult;

                var dtos= _mapper.Map<List<GetInsuranceProviderDto>>(repositoryResult.Result);

                return _operations.SuccessResult(dtos, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "GetFailed"));
                return _operations.HandleException("Operations", "GetFailed");
            }
        }
        
        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var repositoryResult = await _insuranceProviderRepository.GetInsurenProviderById(id);
                if (!repositoryResult.Success) return repositoryResult;


                var dtos = _mapper.Map<GetInsuranceProviderDto>(repositoryResult.Result);

                return _operations.SuccessResult(dtos,"Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "GetFailed"));
                return _operations.HandleException("Operations", "GetFailed");
            }
        }
        public async Task<OperationResult> Remove(RemoveInsuranceProviderDto dto)
        {
            try
            {
                var validationResult = _operations.isNull(dto);
                if (!validationResult.Success)
                {
                    _logger.LogWarning(validationResult.Message);
                    return validationResult;
                }

                var operationResult = await _insuranceProviderRepository.RemoveInsuranceProviderAsync(dto.InsuranceProviderID);

                if (operationResult.Success)
                {
                    dto.Removed = true;
                    return operationResult;
                }

                return operationResult; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "RemoveInsurenProvider"));
                return _operations.HandleException("Insurances", "RemoveInsurenProvider");
            }
        }

        public async Task<OperationResult> Save(SaveInsuranceProviderDto dto)
        {
            try
            {
                var insuranceProvider = _mapper.Map<InsuranceProviders>(dto);

                var result = await _insuranceProviderRepository.SaveEntityAsync(insuranceProvider);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }

        public async Task<OperationResult> Update(UpdateInsuranceProviderDto dto)
        {
            try
            {
                var insuranceProvider = _mapper.Map<InsuranceProviders>(dto);
                
                var result= await _insuranceProviderRepository.UpdateEntityAsync(insuranceProvider);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "UpdateFailed"));
                return _operations.HandleException("Operations", "UpdateFailed");
            }
        }
    }
}
