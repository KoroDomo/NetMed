using Azure;
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
    public class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly NetMedContext _context;
        private readonly ICustomLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly NetworkTypeValidator _operations;

        public NetworkTypeRepository(NetMedContext context,
                                     ICustomLogger logger,
                                     IConfiguration configuration) : base(context, logger, configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = new NetworkTypeValidator(_configuration);
        }

        public override async Task<OperationResult> SaveEntityAsync(NetworkType network)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR=_operations.validateNetworkType(network);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"El network no puede ser nulo.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.SaveEntityAsync");
                }

                _context.NetworkType.Add(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Network agregado exitosamente: " + network.ToString());

                return _operations.SuccessResult(network, "NetworkTypeRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar el Network: {network.ToString}: {operationR.Message}");
                operationR = _operations.HandleException(ex, "NetworkTypeRepository.SaveEntityAsync");

                return _operations.HandleException(ex, "NetworkTypeRepository.SaveEntityAsync");
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(NetworkType network)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR= _operations.validateNetworkType(network);

                if (operationR.Success == false)
                {
                    throw new Exception();
                }

                _context.NetworkType.Update(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha actualizado el Network: " + network.ToString());
                return _operations.SuccessResult(network, "NetworkTypeRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el Network {network.ToString}: : {operationR.Message}.");
                return _operations.HandleException(ex, "NetworkTypeRepository.UpdateEntityAsync");
            }
        }

        public override async Task<OperationResult> GetAllAsync()
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var network = await _context.NetworkType
                    .Where(n => n.IsActive)
                    .OrderByDescending(n => n.CreatedAt)
                    .Select(n => new NetworkTypeModel()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Description = n.Description,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        IsActive = n.IsActive

                    }).ToListAsync();

                operationR = _operations.isNull(network);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los Networks.");
                    return operationR;
                }

                
                return _operations.SuccessResult(network, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Network. : {operationR.Message}.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var networks = await _context.NetworkType
                    .Where(filter)
                    .OrderByDescending(n => n.CreatedAt)
                    .Select(n => new NetworkTypeModel()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Description = n.Description,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        IsActive = n.IsActive

                    }).ToListAsync();

                operationR = _operations.isNull(networks);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los Networks.");
                    return operationR;
                }

                
                return _operations.SuccessResult(networks, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }

        public async Task<OperationResult> RemoveNetworkTypeAsync(int id)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                var network = await _context.NetworkType.FindAsync(id);

                operationR = _operations.isNull(network);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontraron los insuranceProviders.");
                    return operationR;
                }

                network.IsActive = false;
                _context.NetworkType.Update(network);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Network eliminado exitosamente: " + network.ToString());
                return _operations.SuccessResult(null, "NetworkTypeRepository.RemoveNetworkTypeAsync.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Network con el ID: {id}");
                return _operations.HandleException(ex, "NetworkTypeRepository.RemoveNetworkTypeAsync");
            }
        }

        public async Task<OperationResult> GetNetworkTypeById(int networkTypeId)
        {
            OperationResult operationR = new OperationResult();

            try
            {
                var networkTypes = await _context.NetworkType
                    .Where(nt => nt.Id == networkTypeId)
                    .Select(nt => new NetworkTypeModel()
                    {
                        Id = nt.Id,
                        Name = nt.Name,
                        Description = nt.Description,
                        CreatedAt = nt.CreatedAt,
                        UpdatedAt = nt.UpdatedAt,
                        IsActive = nt.IsActive

                    }).ToListAsync();

                operationR = _operations.isNull(networkTypes);

                if (operationR.Success == false)
                {
                    _logger.LogWarning($"No se encontró el Network con el Provider de ID: {networkTypeId} para eliminar.");
                    return operationR;
                }

                return _operations.SuccessResult(networkTypes, "NetworkTypeRepository.GetNetworkTypeById");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Networ conel Provider de ID: {networkTypeId}");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetNetworkTypeById");
            }
        }
    }
}