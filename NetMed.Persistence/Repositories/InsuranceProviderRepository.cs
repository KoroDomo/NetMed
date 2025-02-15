using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repository
{
    public class InsuranceProviderRepository : BaseRepository<InsuranceProvider, int>, IInsuranceProviderRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<InsuranceProviderRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly Operations _operations;

        public InsuranceProviderRepository(NetMedContext context,
                                           ILogger<InsuranceProviderRepository> logger,
                                           IConfiguration configuration, Operations operations) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = operations;
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
                    return _operations.SuccessResult(null,_configuration, "InsuranceProviderRepository.GetInsurenProviderById");
                }

                return _operations.SuccessResult(provider,_configuration, "InsuranceProviderRepository.GetInsurenProviderById");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsurenProviderById",_configuration);
            }
        }

        public async override Task<OperationResult> SaveEntityAsync(InsuranceProvider provider)
        {
            try
            {
                if (provider == null)
                {
                    return _operations.SuccessResult(null, _configuration, "El proveedor de seguros no puede ser nulo.");
                }

                _context.InsuranceProviders.Add(provider);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(provider, _configuration, "InsuranceProviderRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.SaveEntityAsync", _configuration);
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
                    return _operations.SuccessResult(null, _configuration, "InsuranceProviderRepository.GetAllAsync");
                }

                return _operations.SuccessResult(providers, _configuration, "InsuranceProviderRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetAllAsync",_configuration );
            }
        }

        public async override Task<OperationResult> UpdateEntityAsync(InsuranceProvider entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null,_configuration, "La entidad no puede ser nula.");
                }

                _context.InsuranceProviders.Update(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity,_configuration, "InsuranceProviderRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.UpdateEntityAsync",_configuration);
            }
        }

        public async Task<OperationResult> DeleteInsuranceProviderAsync(int id)
        {
            try
            {
                var entity = await _context.InsuranceProviders.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("No se encontró la entidad con el ID: {Id} para eliminar.", id);
                    return _operations.SuccessResult(null,_configuration,"Entidad no encontrada.");
                }

                _context.InsuranceProviders.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Entidad eliminada exitosamente: {Entity}", entity.ToString());
                return _operations.SuccessResult(null, _configuration,"Entidad eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la entidad con el ID: {Id}", id);
                return _operations.HandleException(ex, "Ocurrió un error eliminando la entidad", _configuration);
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
                    return _operations.SuccessResult(null, _configuration,"InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
                }

                return _operations.SuccessResult(providers, _configuration,"InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetPreferredInsuranceProvidersAsync" , _configuration);
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
                    return _operations.SuccessResult(null, _configuration,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
                }

                return _operations.SuccessResult(providers, _configuration,"InsuranceProviderRepository.GetActiveInsuranceProvidersAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetActiveInsuranceProvidersAsync", _configuration);
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
                    return _operations.SuccessResult(null, _configuration, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
                }

                return _operations.SuccessResult(providers, _configuration, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByRegionAsync", _configuration);
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
                    return _operations.SuccessResult(null,_configuration, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
                }

                return _operations.SuccessResult(providers,_configuration, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "InsuranceProviderRepository.GetInsuranceProvidersByMaxCoverageAsync", _configuration);
            }
        }
    }
}