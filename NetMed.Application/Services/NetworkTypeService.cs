using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Validators;

namespace NetMed.Application.Services
{

    public class NetworktypeService : INetworkTypeService
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ICustomLogger _logger;
        private readonly NetworkTypeValidator _operations;
        public NetworktypeService(NetMedContext context,
                                  INetworkTypeRepository repository,
                                  ICustomLogger logger, MessageMapper messageMapper)
        {
            _networkTypeRepository = repository;
            _logger = logger;
            _operations = new NetworkTypeValidator(messageMapper);
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {

                var repositoryResult = await _networkTypeRepository.GetAllAsync();
                if (!repositoryResult.Success) return repositoryResult;

                var dtos = ((List<NetworkTypeModel>)repositoryResult.Result)
                    .Select(n => new NetworkTypeDto
                    {
                        Name = n.Name,
                        Description = n.Description,
                        ChangeDate = n.UpdatedAt
                    })
                    .ToList();

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

                var repositoryResult = await _networkTypeRepository.GetEntityByIdAsync(id);
                if (!repositoryResult.Success) return repositoryResult;

                if (repositoryResult.Result is not List<NetworktypeModel> providers)
                {
                    return _operations.HandleException("Operations", "GetFailed");
                }

                var dtos = ((List<NetworkTypeModel>)repositoryResult.Result)
                    .Select(n => new NetworkTypeDto
                    {
                        Name = n.Name,
                        Description = n.Description,
                        ChangeDate = n.UpdatedAt
                    })
                    .ToList();

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
                if (!validationResult.Success)
                {
                    _logger.LogWarning(validationResult.Message);
                    return validationResult;
                }

                var operationResult = await _networkTypeRepository.RemoveNetworkTypeAsync(dto.NetworkTypeId);

                if (operationResult.Success)
                {
                    dto.Removed = true;
                    dto.ChangeUserID = dto.NetworkTypeId;
                    return _operations.SuccessResult(dto, "Operations", "GetSuccess");
                }

                return operationResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "RemoveInsurenProvider"));
                return _operations.HandleException("Insurances", "RemoveInsurenProvider");
            }
        }

        public async Task<OperationResult> Save(SaveNetworkTypeDto dto)
        {
            try
            {
                var network = new NetworkType
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UpdatedAt = dto.ChangeDate
                };

                var networks = await _networkTypeRepository.SaveEntityAsync(network);
                return _operations.SuccessResult(dto, "Operations", "SaveSuccess");
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
                var network = new NetworkType
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UpdatedAt = dto.ChangeDate
                };

                var networks = await _networkTypeRepository.UpdateEntityAsync(network);
                return _operations.SuccessResult(dto, "Operations", "GetSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }
    }
}
