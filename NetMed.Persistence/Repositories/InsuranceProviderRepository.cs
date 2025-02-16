using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;


namespace NetMed.Persistence.Repository
{
    public class InsuranceProviderRepository : BaseRepository<InsuranceProvider, int>, IInsuranceProviderRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<InsuranceProviderRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly OperationValidator _operations;
        

        public InsuranceProviderRepository(NetMedContext context,
                                           ILogger<InsuranceProviderRepository> logger,
                                           IConfiguration configuration) : base(context, logger,configuration )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = new OperationValidator(_configuration);
            
        }

        public async Task<OperationResult> GetInsurenProviderById(int insurenceProviderId)
        {
            OperationResult operationR = new OperationResult();

           
            try
            {
                var provider = await _context.InsuranceProviders
                    .Where(ip => ip.Id == insurenceProviderId)
                    .Select(ip => new InsuranceProviderModel()
                    {
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .FirstOrDefaultAsync();

                if (provider == null)
                {
                    _logger.LogWarning($"No se encontró el Provider con el ID {insurenceProviderId}.");
                    return _operations.SuccessResult(null, "InsuranceProviderRepository.GetInsurenProviderById");
                }
                else
                {
                    _logger.LogInformation("Se ha obtenido el Provider: " + provider.ToString());
                    return _operations.SuccessResult(provider, "Succes: InsuranceProviderRepository.GetInsurenProviderById");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el provider con el ID: {insurenceProviderId} ");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsurenProviderById");
            }
        }

        public async override Task<OperationResult> SaveEntityAsync(InsuranceProvider provider)
        {
            try
            {
                if (provider == null)
                {
                    return _operations.SuccessResult(null, "El proveedor de seguros no puede ser nulo.");
                }

                _context.InsuranceProviders.Add(provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha guardado el Provider: " + provider.ToString());

                return _operations.SuccessResult(provider, "Succes: InsuranceProviderRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar el Provider: {provider} ");

                return _operations.HandleException(ex, "InsuranceProviderRepository.SaveEntityAsync");
            }
        }

        public async override Task<OperationResult> GetAllAsync()
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsActive)
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new InsuranceProviderModel()
                    {
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .ToListAsync();

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null, "InsuranceProviderRepository.GetAllAsync");
                }
                
                return _operations.SuccessResult(providers, "Succes: InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers.");

                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync" );
            }
        }

        public async override Task<OperationResult> UpdateEntityAsync(InsuranceProvider entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null, "La entidad no puede ser nula.");
                }

                _context.InsuranceProviders.Update(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha actualizado el Provider: " + entity.ToString());
                return _operations.SuccessResult(entity, "Succes: InsuranceProviderRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el Provider {entity}.");
                return _operations.HandleException(ex, "Error: InsuranceProviderRepository.UpdateEntityAsync");
            }
        }

        public async Task<OperationResult> DeleteInsuranceProviderAsync(int id)
        {
            try
            {
                var entity = await _context.InsuranceProviders.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"No se encontró el provider con el ID: {id} para eliminar.");
                    return _operations.SuccessResult(null, $"NotFound: InsuranceProviderRepository.DeleteInsuranceProviderAsync.");
                }

                _context.InsuranceProviders.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Provider eliminado exitosamente: " + entity.ToString());
                return _operations.SuccessResult(null, "Succes: InsuranceProviderRepository.DeleteInsuranceProviderAsync.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Provider con el ID: {id}");
                return _operations.HandleException(ex, "Error: InsuranceProviderRepository.DeleteInsuranceProviderAsync.");
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
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .ToListAsync();

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null,"InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
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
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.IsActive)
                    .Select(ip => new InsuranceProviderModel()
                    {
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .ToListAsync();

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
                }

                return _operations.SuccessResult(providers,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
        }

        public async Task<OperationResult> GetInsuranceProvidersByRegionAsync(string region)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.State == region || ip.City == region)
                    .Select(ip => new InsuranceProviderModel()
                    {
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .ToListAsync();

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
            }
        }

        public async Task<OperationResult> GetInsuranceProvidersByMaxCoverageAsync(decimal maxCoverage)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.MaxCoverageAmount <= maxCoverage)
                    .Select(ip => new InsuranceProviderModel()
                    {
                        InsuranceProviderID = ip.Id,
                        ContactNumber = ip.PhoneNumber,
                        Email = ip.Email,
                        Website = ip.Website,
                        Address = ip.Address,
                        City = ip.City,
                        State = ip.State,
                        Country = ip.Country,
                        ZipCode = ip.ZipCode,
                        CoverageDetails = ip.CoverageDetails,
                        MaxCoverageAmount = ip.MaxCoverageAmount,
                        IsPrefered = ip.IsPreferred,
                        IsActive = ip.IsActive,
                    })
                    .ToListAsync();

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
            }
        }
    }
}