using AutoMapper;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.NetworkType;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators.Implementations;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{

    public class NetworktypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ICustomLogger _logger;
        private readonly INetworkTypeValidator _operations;
        private readonly IMapper _mapper;
        public NetworktypeService(INetworkTypeRepository repository,
                                  ICustomLogger logger, 
                                  IMapper mapper)
        {
            _networkTypeRepository = repository;
            _logger = logger;
            _operations = new NetworkTypeValidator();
            _mapper = mapper;
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {

                var repositoryResult = await _networkTypeRepository.GetAllAsync();

                if (repositoryResult.Success == false) return repositoryResult;

                var dtos = _mapper.Map<List<GetNetworkTypeDto>>(repositoryResult.Result);

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

                var repositoryResult = await _networkTypeRepository.GetNetworkTypeById(id);
                if (!repositoryResult.Success) return repositoryResult;

                var dtos = _mapper.Map<NetworkTypeDto>(repositoryResult.Result);

                return _operations.SuccessResult(dtos, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "GetFailed"));

                return _operations.HandleException("Operations", "GetFailed");
            }
        }
        public async Task<OperationResult> Remove(RemoveNetworkTypeDto dto)
        {
            try
            {
                var validationResult = _operations.isNull(dto);
                if (validationResult.Success == false)
                {
                    _logger.LogWarning(validationResult.Message);
                    return validationResult;
                }

                var operationResult = await _networkTypeRepository.RemoveNetworkTypeAsync(dto.NetworkTypeId);

                if (operationResult.Success)
                {
                    operationResult.Result.dto.Removed = true;
                    return operationResult;
                }

                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Networks", "RemoveNetworkType"));
                return _operations.HandleException("Networks", "RemoveNetworkType");
            }
        }

        public async Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            try
            {
                var network = _mapper.Map<NetworkType>(dto);

                var result = await _networkTypeRepository.SaveEntityAsync(network);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }

        public async Task<OperationResult> Update(UpdateNetworkTypeDto dto)
        {
            try
            {
                var network = _mapper.Map<NetworkType>(dto);

                var result = await _networkTypeRepository.UpdateEntityAsync(network);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }
    }
}
