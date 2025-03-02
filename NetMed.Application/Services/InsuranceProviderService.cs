using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Application.Contracts;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetMed.Model.Models;
using NetMed.Persistence.Context;

namespace NetMed.Application.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        private readonly NetMedContext _context;
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ILogger<InsuranceProviderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly OperationValidator _operations;
        public InsuranceProviderService(NetMedContext context,
                                        IInsuranceProviderRepository repository, 
                                        ILogger<InsuranceProviderService> logger, IConfiguration configuration)
        {
            _context = context;
            _insuranceProviderRepository = repository;
            _logger = logger;
            _configuration = configuration;
            _operations = new OperationValidator(_configuration);
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var insuranceProviders = await _insuranceProviderRepository.GetAllAsync();
                return _operations.SuccessResult(insuranceProviders, "InsuranceProviderService:GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al obtener todos los Providers.");
                return _operations.HandleException(ex, "InsuranceProviderService:GetAll");
            }

        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var providers = await _context.InsuranceProviders
                    .Where(ip => ip.Id == id)
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(ip => new InsuranceProviderDto()
                    {
                        Name = ip.Name,
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

                if (providers == null || !providers.Any())
                {
                    return _operations.SuccessResult(null, "InsuranceProviderService.GetById");
                }

                return _operations.SuccessResult(providers, "InsuranceProviderService.GetById");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers.");

                return _operations.HandleException(ex, "InsuranceProviderService.GetById");
            }
        }
        public Task<OperationResult> Remove(RemoveInsuranceProviderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveInsuranceProviderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateInsuranceProviderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
