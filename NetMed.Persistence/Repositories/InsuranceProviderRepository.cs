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

        public InsuranceProviderRepository(NetMedContext context, 
                                           ILogger<InsuranceProviderRepository> logger, 
                                           IConfiguration configuration) : base(context) 
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetInsurenProviderById(int insurenceProviderId)
        {
            OperationResult result = new OperationResult();

            try
            {
                var querys = await( from insuranceProvider in _context.InsuranceProviders
                             where insuranceProvider.Id == insurenceProviderId
                             select new InsuranceProviderModel()
                             {
                                 InsuranceProviderID = insurenceProviderId,
                                 ContactNumber = insuranceProvider.PhoneNumber,
                                 Email = insuranceProvider.Email,
                                 Website = insuranceProvider.Website,
                                 Address = insuranceProvider.Address,
                                 City = insuranceProvider.City,
                                 State = insuranceProvider.State,
                                 Country = insuranceProvider.Country,
                                 ZipCode = insuranceProvider.ZipCode,
                                 CoverageDetails = insuranceProvider.CoverageDetails,
                                 MaxCoverageAmount = insuranceProvider.MaxCoverageAmount,
                                 IsPrefered = insuranceProvider.IsPreferred,
                                 IsActive = insuranceProvider.IsActive,
                             }).ToListAsync();
            }
            catch (Exception ex)
            {
                result.Mesagge = _configuration["Error:InsuranceProviderRepository.GetInsurenProviderById"];
                result.Success = false;
                _logger.LogError(result.Mesagge,ex.ToString());

            }
            return result;
        }
        // Aqui hay que elaborar una interfaz para implementar solamente los metodos necesarios
        public async override Task<OperationResult> SaveEntityAsync(InsuranceProvider entity) 
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.PhoneNumber == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El número de telefono es requerido.";
                return operationResult;
            }

            if (entity.Email == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El email es requerido";
                return operationResult;
            }

            if (entity.Website == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El url del website es requerido.";
                return operationResult;
            }

            if (entity.Address == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "La direccion es requerida.";
                return operationResult;
            }
            if (entity.City == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "La direccion es requerida.";
                return operationResult;
            }
            if (entity.State == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El estado es requerido.";
                return operationResult;
            }
            if (entity.Country == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El pais es requerido.";
                return operationResult;
            }
            if (entity.ZipCode == null)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "El codigo zip es requerido.";
                return operationResult;
            }
            if (entity.MaxCoverageAmount <= 0)
            {
                operationResult.Success = false;
                operationResult.Mesagge = "La covertura maxima no puede ser menor que cero.";
                return operationResult;
            }
            
            return operationResult;
        }

        public override Task<bool> ExistsAsync(Expression<Func<InsuranceProvider, bool>> filter)
        {
            return base.ExistsAsync(filter);
        }
        public override Task<OperationResult> GetAllAsync(Expression<Func<InsuranceProvider, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }
        public async override Task<OperationResult> GetAllAsync()
        {
            OperationResult result = new OperationResult();

            try
            {

                result.Result = await(from insuranceProvider in _context.InsuranceProviders
                                    where insuranceProvider.IsActive == true
                                    orderby insuranceProvider.CreatedAt descending
                                    select new InsuranceProviderModel()
                                    {
                                        InsuranceProviderID = insuranceProvider.Id,
                                        ContactNumber = insuranceProvider.PhoneNumber,
                                        Email = insuranceProvider.Email,
                                        Website = insuranceProvider.Website,
                                        Address = insuranceProvider.Address,
                                        City = insuranceProvider.City,
                                        State = insuranceProvider.State,
                                        Country = insuranceProvider.Country,
                                        ZipCode = insuranceProvider.ZipCode,
                                        CoverageDetails = insuranceProvider.CoverageDetails,
                                        MaxCoverageAmount = insuranceProvider.MaxCoverageAmount,
                                        IsPrefered= insuranceProvider.IsPreferred,
                                        IsActive = insuranceProvider.IsActive,

                                    }).ToListAsync();

            }
            catch (Exception ex)
            {
                result.Mesagge = _configuration["Error:InsuranceProviderRepository.GetInsurenProviderById"];
                result.Success = false;
                _logger.LogError(result.Mesagge, ex.ToString());
            }
            return result;
        }
        public override Task<OperationResult> UpdateEntityAsync(InsuranceProvider entity)
        {
            return base.UpdateEntityAsync(entity);
        }

        public Task<OperationResult> DeleteInsuranceProviderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetPreferredInsuranceProvidersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetActiveInsuranceProvidersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetInsuranceProvidersByRegionAsync(string region)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetInsuranceProvidersByMaxCoverageAsync(decimal maxCoverage)
        {
            throw new NotImplementedException();
        }
    }
}
