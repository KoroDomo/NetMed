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
        private readonly IConfiguration _configuration;
        private readonly InsuranceProviderValidator _operations;
        
        public InsuranceProviderRepository(NetMedContext context,
                                           ICustomLogger logger,
                                           IConfiguration configuration) : base(context, logger,configuration )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = new InsuranceProviderValidator(_configuration);
            
        }

        public async Task<OperationResult> GetInsurenProviderById(int InsuranceId)
        {
            OperationResult operationR = new OperationResult();

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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontró el insuranceProvider con el ID: {InsuranceId} ");
                    return operationR;
                }
                
                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsurenProviderById");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el insuranceProvider con el ID: {InsuranceId}: {operationR.Message} ");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsurenProviderById");
            }
        }

        public async override Task<OperationResult> SaveEntityAsync(InsuranceProviders provider)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR = _operations.validateInsuranceProvider(provider);

                if (operationR.Success == false)
                {
                    throw new Exception();
                }

                _context.InsuranceProviders.Add(provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha guardado el Provider: " + provider.ToString());

                return _operations.SuccessResult(provider, "InsuranceProviderRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar el Provider: {provider.ToString}: {operationR.Message}");
                operationR= _operations.HandleException(ex, "InsuranceProviderRepository.SaveEntityAsync");

                return _operations.HandleException(ex, "InsuranceProviderRepository.SaveEntityAsync");
            }
        }

        public async override Task<OperationResult> GetAllAsync()
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsActive)
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProviders.");
                    return operationR;
                }

                
                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers : {operationR.Message}.");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync" );
            }
        }
        public async override Task<OperationResult> GetAllAsync(Expression<Func<InsuranceProviders, bool>> filter)
        {
            OperationResult operationR = new OperationResult();
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los InsuranceProviders.");
                    return operationR;
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers.");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync");
            }
        }

        public async override Task<OperationResult> UpdateEntityAsync(InsuranceProviders providers)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR = _operations.validateInsuranceProvider(providers);
                
                if (operationR.Success == false)
                {
                    throw new Exception();
                }

                _context.InsuranceProviders.Update(providers);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha actualizado el Provider: " + providers.ToString());
                return _operations.SuccessResult(providers, "InsuranceProviderRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el Provider {providers.ToString}: : {operationR.Message}.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.UpdateEntityAsync");
            }
        }

        public async Task<OperationResult> RemoveInsuranceProviderAsync(int id)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var provider = await _context.InsuranceProviders.FindAsync(id);

                operationR = _operations.isNull(provider);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProviders.");
                    return operationR;
                }

                provider.IsActive = false;
                _context.InsuranceProviders.Update(provider);
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

        public async Task<OperationResult> GetPreferredInsuranceProvidersAsync()
        {
            OperationResult operationR = new OperationResult();
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProvider con preferencia.");
                    return operationR;
                }
                return _operations.SuccessResult(providers,"InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Providers con preferencia");
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProvider activos.");
                    return operationR;
                }

                return _operations.SuccessResult(providers,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Providers.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
        }

        public async Task<OperationResult> GetInsuranceProvidersByRegionAsync(string region)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.State == region || ip.City == region)
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProvider activos.");
                    return operationR;
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Providers.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
            }
        }

        public async Task<OperationResult> GetInsuranceProvidersByMaxCoverageAsync(decimal maxCoverage)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.MaxCoverageAmount <= maxCoverage)
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No hay InsuranceProviders con esa covertura.");
                    return operationR;
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Providers.");
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
            }
        }
    }
}