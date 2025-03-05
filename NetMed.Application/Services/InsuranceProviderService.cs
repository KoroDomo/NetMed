using Microsoft.Extensions.Configuration;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Application.Contracts;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.Persistence.Validators;
using NetMed.Domain.Entities;

namespace NetMed.Application.Services
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        private readonly NetMedContext _context;
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        private readonly ICustomLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly InsuranceProviderValidator _operations;
        
        public InsuranceProviderService(NetMedContext context,
                                        IInsuranceProviderRepository repository,
                                        ICustomLogger logger, IConfiguration configuration)
        {
            _context = context;
            _insuranceProviderRepository = repository;
            _logger = logger;
            _configuration = configuration;
            _operations = new InsuranceProviderValidator(_configuration);
            
        }

        public async Task<OperationResult> GetAll()
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var providers = await _context.InsuranceProviders
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProviders. ");
                    return operationR;
                }

                return _operations.SuccessResult(providers, "InsuranceProviderService.GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todos los Providers: {operationR.Message}.");

                return _operations.HandleException(ex, "InsuranceProviderService.GetAll");
            }

        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult operationR = new OperationResult();
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

                operationR = _operations.isNull(providers);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontró el insuranceProvider con el ID: {id} ");
                    return operationR;
                }

                return _operations.SuccessResult(providers, "InsuranceProviderService.GetById");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Providers.");

                return _operations.HandleException(ex, "InsuranceProviderService.GetById");
            }
        }
        public async Task<OperationResult> Remove(int id)
        {
            try
            {
                var insuranceProviders = await _insuranceProviderRepository.RemoveInsuranceProviderAsync(id);
                return _operations.SuccessResult(insuranceProviders, "InsuranceProviderService:Remove");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al remover el Provider.");
                return _operations.HandleException(ex, "InsuranceProviderService:Remove");
            }
        }

        public async Task<OperationResult> Save(SaveInsuranceProviderDto dto)
        {
            try
            {
                var insuranceProvider = new InsuranceProviders
                {
                    Name = dto.Name,
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
                return _operations.SuccessResult(insuranceProviders, "InsuranceProviderService:Save");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al guardar el Provider.");
                return _operations.HandleException(ex, "InsuranceProviderService:Save");
            }
        }

        public async Task<OperationResult> Update(UpdateInsuranceProviderDto dto)
        {
            try
            {
                var insuranceProvider = new InsuranceProviders
                {
                    Name = dto.Name,
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
                return _operations.SuccessResult(insuranceProviders, "InsuranceProviderService:Update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al actualizar el Provider.");
                return _operations.HandleException(ex, "InsuranceProviderService:Update");
            }
        }
    }
}
