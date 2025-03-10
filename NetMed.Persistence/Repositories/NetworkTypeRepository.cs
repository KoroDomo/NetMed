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
        private readonly NetworkTypeValidator _operations;

        public NetworkTypeRepository(NetMedContext context,
                                     ICustomLogger logger,
                                     IConfiguration configuration) : base(context, logger, configuration)
        {
            _operations = new NetworkTypeValidator(configuration);
        }

        public override async Task<OperationResult> SaveEntityAsync(NetworkType network)
        {
            OperationResult operationR = new OperationResult();
            try
            {
                operationR =_operations.ValidateNetworkType(network);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                operationR = _operations.ValidateNetworkTypeNameExists(network, _context);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                _context.NetworkType.Add(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha guardado el Network: " + network.ToString());

                return _operations.SuccessResult(network, "NetworkTypeRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al guardar el Network: {network.ToString}: {operationR.Message}");

                return _operations.HandleException(ex, "NetworkTypeRepository.SaveEntityAsync");
            }
        }

        public async Task<OperationResult> RemoveNetworkTypeAsync(int id)
        {
            NetworkType networkType;
            try
            {
                var entity = await GetNetworkTypeById(id);
                networkType = entity.Result;

                networkType.IsActive = false;

                await UpdateEntityAsync(networkType);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Network eliminado exitosamente: " + networkType.ToString());
                return _operations.SuccessResult(null, "NetworkTypeRepository.RemoveNetworkTypeAsync.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Network con el ID: {id}");
                return _operations.HandleException(ex, "NetworkTypeRepository.RemoveNetworkTypeAsync");
            }
        }
        public override async Task<OperationResult> UpdateEntityAsync(NetworkType network)
        {
            OperationResult operationR;
            try
            {
                operationR = _operations.ValidateNetworkTypeNameExists(network, _context);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                operationR = _operations.ValidateNetworkType(network);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }
               
                _context.NetworkType.Update(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Se ha actualizado el Network: " + network.ToString());
                return _operations.SuccessResult(network, "NetworkTypeRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el Network {network.ToString}: : {ex.Message}.");
                return _operations.HandleException(ex, "NetworkTypeRepository.UpdateEntityAsync");
            }
        }

        public async Task<OperationResult> GetNetworkTypeById(int networkTypeId)
        {
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


                if (!networkTypes.Any())
                {
                    _logger.LogWarning($"No se encontró el Network con el Provider de ID: {networkTypeId}.");
                    return _operations.HandleException(null, "NetworkTypeRepository.GetNetworkTypeById"); ;
                }

                return _operations.SuccessResult(networkTypes, "NetworkTypeRepository.GetNetworkTypeById");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Networ conel Provider de ID: {networkTypeId}");

                return _operations.HandleException(ex, "NetworkTypeRepository.GetNetworkTypeById");
            }
        }
        public override async Task<OperationResult> GetAllAsync()
        {
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

                if (!network.Any())
                {
                    _logger.LogWarning("No hay Networks.");
                    return _operations.HandleException(null, "No hay Networks.");
                }

                return _operations.SuccessResult(network, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Network. : {ex.Message}.");

                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
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

                if (!networks.Any())
                {
                    _logger.LogWarning("No hay Networks activos.");
                    return _operations.HandleException(null, "NetworkTypeRepository.GetAllAsync");
                }

                return _operations.SuccessResult(networks, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }


    }
}