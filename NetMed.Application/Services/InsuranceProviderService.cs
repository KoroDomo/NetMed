using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Application.Contracts;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Persistence.Validators;
using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Application.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ICustomLogger _logger;
        private readonly InsuranceProviderValidator _operations;

        public InsuranceProviderService(NetMedContext context,
                                        IInsuranceProviderRepository repository,
                                        ICustomLogger logger, MessageMapper messageMapper)
        {
            _insuranceProviderRepository = repository;
            _logger = logger;
            _operations = new InsuranceProviderValidator(messageMapper);
            
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {
                var repositoryResult = await _insuranceProviderRepository.GetAllAsync();

                if (!repositoryResult.Success) return repositoryResult;

                if (repositoryResult.Result is not List<NetworktypeModel> providers)
                {
                    return _operations.HandleException("Entitys", "NullEntity");
                }

                var dtos = ((List<NetworktypeModel>)repositoryResult.Result)
                    .Where(ip => ip.IsActive == true)
                    .Select(ip => new InsuranceProviderDto
                    {
                        Name = ip.Name,
                        PhoneNumber = ip.ContactNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        IsPreferred = ip.IsPreferred,
                        NetworkTypeID = ip.NetworkTypeID,
                        AcceptedRegions = ip.AcceptedRegions,
                        MaxCoverageAmount = ip.MaxCoverageAmount
                        
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
                var repositoryResult = await _insuranceProviderRepository.GetEntityByIdAsync(id);
                if (!repositoryResult.Success) return repositoryResult;

                if (repositoryResult.Result is not List<NetworktypeModel> providers)
                {
                    return _operations.HandleException("Operations", "GetFailed");
                }

                var dtos = ((List<NetworktypeModel>)repositoryResult.Result)
                    .Where(ip => ip.IsActive == true)
                    .Select(ip => new InsuranceProviderDto
                    {
                        
                        Name = ip.Name,
                        PhoneNumber = ip.ContactNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        IsPreferred = ip.IsPreferred,
                        NetworkTypeID = ip.NetworkTypeID,
                        AcceptedRegions = ip.AcceptedRegions,
                        MaxCoverageAmount = ip.MaxCoverageAmount
                    })
                    .ToList();

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
                    dto.ChangeUserID = dto.InsuranceProviderID;
                    return _operations.SuccessResult(dto, "Insurances", "RemoveInsurenProvider");
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
                var insuranceProvider = new InsuranceProviders
                {
                    Id = dto.NetworkTypeID,
                    Name = dto.Name,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    Website = dto.Website,
                    Address = dto.Address,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    ZipCode = dto.ZipCode,
                    CoverageDetails = dto.CoverageDetails,
                    IsPreferred = dto.IsPreferred,
                    NetworkTypeID = dto.NetworkTypeID,
                    AcceptedRegions = dto.AcceptedRegions,
                    MaxCoverageAmount = dto.MaxCoverageAmount
                };

                var insuranceProviders = await _insuranceProviderRepository.SaveEntityAsync(insuranceProvider);
                return _operations.SuccessResult(dto, "Operations", "SaveSuccess");
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
                var insuranceProvider = new InsuranceProviders
                {
                    Id= dto.InsuranceProviderID,
                    Name = dto.Name,
                    PhoneNumber= dto.PhoneNumber,
                    Email = dto.Email,
                    Website = dto.Website,
                    Address = dto.Address,
                    City = dto.City,
                    State = dto.State,
                    Country = dto.Country,
                    ZipCode = dto.ZipCode,
                    CoverageDetails = dto.CoverageDetails,
                    IsPreferred = dto.IsPreferred,
                    NetworkTypeID = dto.NetworkTypeID,
                    AcceptedRegions = dto.AcceptedRegions,
                    MaxCoverageAmount = dto.MaxCoverageAmount
                };

                var insuranceProviders = await _insuranceProviderRepository.UpdateEntityAsync(insuranceProvider);
                return _operations.SuccessResult(dto, "Operations", "UpdateSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }
    }
}
