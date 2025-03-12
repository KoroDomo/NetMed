using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class InsuranceProviderRepository : BaseRepository<InsuranceProviders>, IInsuranceProviderRepository
    {
        private readonly NetMedContext _context;
        private readonly ICustomLogger _logger;
        private readonly InsuranceProviderValidator _operations;


        public InsuranceProviderRepository(NetMedContext context, 
                                           ICustomLogger logger,
                                           MessageMapper messageMapper) : base(context, logger, messageMapper)
        {
            _context = context;
            _logger = logger;
            _operations = new InsuranceProviderValidator(messageMapper);
        }

        public async override Task<OperationResult> SaveEntityAsync(InsuranceProviders provider)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR = _operations.ValidateNameExists(provider, _context);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                operationR = _operations.ValidateInsuranceProvider(provider);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                _context.InsuranceProviders.Add(provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Operations", "SaveSuccess"));

                return _operations.SuccessResult(provider, "Insurances", "SaveSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));

                return _operations.HandleException("Operations", "SaveFailed");
            }
        }

        public async Task<OperationResult> RemoveInsuranceProviderAsync(int id)
        {
            try
            {
                var provider = await _context.InsuranceProviders.FindAsync(id);

                if (provider == null)
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                provider.IsActive = false;

                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Insurances", "RemoveInsurenProvider"));

                return _operations.SuccessResult(provider, "Insurances", "RemoveInsurenProvider");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "RemoveInsurenProvider"));

                return _operations.HandleException("Insurances", "RemoveInsurenProvider");
            }
        }

        public async override Task<OperationResult> UpdateEntityAsync(InsuranceProviders provider)
        {
            try
            {
                var result = _operations.ValidateInsuranceProvider(provider); 
                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    return result;
                }

                var Provider = await _context.InsuranceProviders.FindAsync(provider.Id);
                if (Provider == null)
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                _context.Entry(Provider).CurrentValues.SetValues(provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Operations", "SaveSuccess"));
                return _operations.SuccessResult(provider, "Operations", "SaveSuccess");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
            }
        }

        public async Task<OperationResult> GetInsurenProviderById(int InsuranceId)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.Id == InsuranceId)
                    .Select(ip => new NetworktypeModel()
                    {
                        Id = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.PhoneNumber,
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

                    }).ToListAsync();

                
                if (!providers.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(providers, "Insurances", "GetInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");


            }
        }

        public async override Task<OperationResult> GetAllAsync()
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new NetworktypeModel()
                    {
                        Id = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.PhoneNumber,
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
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsActive = ip.IsActive

                    }).ToListAsync();

                if (!providers.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");

                }
                return _operations.SuccessResult(providers, "Insurances", "GetInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");
            }
        }

        public async override Task<OperationResult> GetAllAsync(Expression<Func<InsuranceProviders, bool>> filter)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(filter)
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new NetworktypeModel()
                    {
                        Id = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.PhoneNumber,
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

                    }).ToListAsync();
                if (!providers.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(providers, "Insurances", "GetInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");
            }
        }

        public async Task<OperationResult> GetPreferredInsuranceProvidersAsync()
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsPreferred)
                    .Select(ip => new NetworktypeModel()
                    {
                        Id = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.PhoneNumber,
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
                    }).ToListAsync();

                if (!providers.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(providers, "Insurances", "GetPreferredInsuranceProviders");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Entitys", "NotFound"));

                return _operations.HandleException("Entitys", "NotFound");
            }
        }

        public async Task<OperationResult> GetActiveInsuranceProvidersAsync()
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsActive)
                    .Select(ip => new NetworktypeModel()
                    {
                        Id = ip.Id,
                        Name = ip.Name,
                        ContactNumber = ip.PhoneNumber,
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
                    }).ToListAsync();

                if (!providers.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(providers, "Insurances", "GetPreferredInsuranceProviders");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Entitys", "NotFound"));
                return _operations.HandleException("Entitys", "NotFound");
            }
        }
    }
}