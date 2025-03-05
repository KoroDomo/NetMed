using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Validators;

namespace NetMed.Application.Services
{

    public class NetworktypeService : INetworkTypeService
    {
        private readonly NetMedContext _context;
        private readonly INetworkTypeRepository _networkTypeRepository;
        private readonly ICustomLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly NetworkTypeValidator _operations;
        public NetworktypeService(NetMedContext context,
                                  INetworkTypeRepository repository,
                                  ICustomLogger logger, IConfiguration configuration)
        {
            _context = context;
            _networkTypeRepository = repository;
            _logger = logger;
            _configuration = configuration;
            _operations = new NetworkTypeValidator(_configuration);
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult operationR = new OperationResult();
            try
            {

                var networks = await _context.NetworkType
                    .Select(n => new NetworkTypeDto()
                    {
                        Name = n.Name,
                        Description = n.Description,
                        ChangeDate = n.UpdatedAt
                        
                    }).ToListAsync();

                operationR = _operations.isNull(networks);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los networks.");
                    return operationR;
                }

                return _operations.SuccessResult(networks, "NetworktypeService.GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Providers.");

                return _operations.HandleException(ex, "NetworktypeService.GetAll");
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult operationR = new OperationResult();
            try
            {

                var networks = await _context.NetworkType
                    .Where(ip => ip.Id == id)
                    .OrderByDescending(ip => ip.CreatedAt)
                    .Select(n => new NetworkTypeDto()
                    {
                        Name = n.Name,
                        Description = n.Description,
                        ChangeDate = n.UpdatedAt

                    }).ToListAsync();

                operationR = _operations.isNull(networks);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontró el network con el ID: {id}");
                    return operationR;
                }

                return _operations.SuccessResult(networks, "NetworktypeService.GetById");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Providers.");

                return _operations.HandleException(ex, "NetworktypeService.GetById");
            }
        }
        public async Task<OperationResult> Remove(int id)
        {
            try
            {
                var networks = await _networkTypeRepository.RemoveNetworkTypeAsync(id);
                return _operations.SuccessResult(networks, "NetworktypeService:Remove");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al remover el Network.");
                return _operations.HandleException(ex, "NetworktypeService:Remove");
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
                return _operations.SuccessResult(networks, "NetworktypeService:Save");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al guardar el Network.");
                return _operations.HandleException(ex, "NetworktypeService:Save");
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
                return _operations.SuccessResult(networks, "NetworktypeService:Update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en el servicio al guardar el Network.");
                return _operations.HandleException(ex, "NetworktypeService:Update");
            }
        }
    }
}
