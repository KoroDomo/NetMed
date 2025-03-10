using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
                                           IConfiguration configuration): base(context, logger, configuration)
        {
            _context = context;
            _logger = logger;
            _operations = new InsuranceProviderValidator(configuration);
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

                _logger.LogInformation("Se ha guardado el Provider: " + provider.ToString());

                return _operations.SuccessResult(provider, "InsuranceProviderRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar el Provider: {provider.ToString}: {operationR.Message}");

                return _operations.HandleException(ex, "InsuranceProviderRepository.SaveEntityAsync");
            }
        }

        public async Task<OperationResult> RemoveInsuranceProviderAsync(int id)
        {
            InsuranceProviders provider;
            try
            {
                var entity = await GetInsurenProviderById(id);
                provider = entity.Result;

                provider.IsActive = false;

                await UpdateEntityAsync(provider);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Provider eliminado exitosamente: " + provider.ToString());
                return _operations.SuccessResult(null, "InsuranceProviderRepository.RemoveInsuranceProviderAsync.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Provider con el ID: {id}");
                return _operations.HandleException(ex, "InsuranceProviderRepository.RemoveInsuranceProviderAsync.");
            }
        }

        public async override Task<OperationResult> UpdateEntityAsync(InsuranceProviders provider)
        {
            OperationResult operationR;
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

                _context.InsuranceProviders.Update(provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha actualizado el Provider: " + provider.ToString());
                return _operations.SuccessResult(provider, "InsuranceProviderRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el Provider {provider.ToString}: : {ex.Message}.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.UpdateEntityAsync");
            }
        }

        public async Task<OperationResult> GetInsurenProviderById(int InsuranceId)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.Id == InsuranceId)
                    .Select(ip => new InsuranceProviderModel()
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
                    _logger.LogWarning($"No se encontró el insuranceProvider con el ID: {InsuranceId}.");
                    return _operations.HandleException(null, "InsuranceProviderRepository.GetInsurenProviderById");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsurenProviderById");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el insuranceProvider con el ID: {InsuranceId}: {ex.Message}");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsurenProviderById");
            }
        }

        public async override Task<OperationResult> GetAllAsync()
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new InsuranceProviderModel()
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
                    _logger.LogWarning("No hay Proveedores.");
                    return _operations.HandleException(null, "InsuranceProviderRepository.GetAllAsync");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers : {ex.Message}.");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync" );
            }
        }

        public async override Task<OperationResult> GetAllAsync(Expression<Func<InsuranceProviders, bool>> filter)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(filter)
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new InsuranceProviderModel()
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
                    _logger.LogWarning("No hay insuranceProviders que cumplan el filtro.");
                    return _operations.HandleException(null, "InsuranceProviderRepository.GetAllAsync");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers.");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync");
            }
        }

        public async Task<OperationResult> GetPreferredInsuranceProvidersAsync()
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsPreferred)
                    .Select(ip => new InsuranceProviderModel()
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
                    _logger.LogWarning($"No se encontraron insuranceProviders con preferencia.");
                    return _operations.HandleException(null, "InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync"); ;
                }

                return _operations.SuccessResult(providers,"InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener insuranceProvider con preferencia");
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
            }
        }

        public async Task<OperationResult> GetActiveInsuranceProvidersAsync()
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsActive)
                    .Select(ip => new InsuranceProviderModel()
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
                    _logger.LogWarning($"No se encontraron insuranceProviders activos.");
                    return _operations.HandleException(null, "InsuranceProviderRepository.GetActiveInsuranceProvidersAsync"); ;
                }

                return _operations.SuccessResult(providers,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener insuranceProvider.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
        }
    }
}