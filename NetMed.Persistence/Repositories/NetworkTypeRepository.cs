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

namespace NetMed.Persistence.Repositories
{
    public class NetworkTypeRepository : BaseRepository<NetworkType, int>, INetworkTypeRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<NetworkTypeRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly OperationValidator _operations;

        public NetworkTypeRepository(NetMedContext context,
                                     ILogger<NetworkTypeRepository> logger,
                                     IConfiguration configuration) : base(context, logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _operations = new OperationValidator(_configuration);

        }

        public override async Task<OperationResult> SaveEntityAsync(NetworkType network)
        {
            try
            {
                if (network == null)
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
                _logger.LogError(ex, $"Error al guardar el Network: {network.ToString}");
                return _operations.HandleException(ex, "NetworkTypeRepository.SaveEntityAsync");
            }
        }

        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            try
            {
                var network = await _context.NetworkType
                    .Where(n => n.Id == id)
                    .Select(n => new NetworkTypeModel()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Description = n.Description,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        IsActive = n.IsActive,
                    }).ToListAsync();

                if (network == null || !network.Any())
                {
                    _logger.LogWarning($"No se encontro ningun Network con el ID: {id}.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.GetEntityByIdAsync");
                }

                
                return _operations.SuccessResult(network, "NetworkTypeRepository.GetEntityByIdAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetEntityByIdAsync");
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(NetworkType network)
        {
            try
            {
                if (network == null)
                {
                    _logger.LogWarning($"El Networ no puede ser nulo.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.UpdateEntityAsync");
                }

                _context.NetworkType.Update(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Network actualizada exitosamente: " + network.ToString());
                return _operations.SuccessResult(network, "NetworkTypeRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.UpdateEntityAsync");
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
                        IsActive = n.IsActive,
                    }).ToListAsync();

                if (network == null || !network.Any())
                {
                    _logger.LogWarning("No se encontro ningun Network.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.GetAllAsync");
                }

                _logger.LogInformation("Network obtenida exitosamente: " + network.ToString());
                return _operations.SuccessResult(network, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
            try
            {
                var entities = await _context.NetworkType
                    .Where(filter)
                    .OrderByDescending(n => n.CreatedAt)
                    .Select(n => new NetworkTypeModel()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Description = n.Description,
                        CreatedAt = n.CreatedAt,
                        UpdatedAt = n.UpdatedAt,
                        IsActive = n.IsActive,
                    }).ToListAsync();

                if (entities == null || !entities.Any())
                {
                    _logger.LogWarning("No se encontro ningun Network.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.GetAllAsync");
                }

                _logger.LogInformation("Network obtenida exitosamente: ");
                return _operations.SuccessResult(entities, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Network.");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync");
            }
        }

        public async Task<OperationResult> DeleteNetworkTypeAsync(int id)
        {
            try
            {
                var entity = await _context.NetworkType.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning($"No se encontró el Network con el ID: {id} para eliminar.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.DeleteNetworkTypeAsync");
                }

                _context.NetworkType.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Network eliminado exitosamente: "+ entity.ToString());
                return _operations.SuccessResult(null, "NetworkTypeRepository.DeleteNetworkTypeAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el Network con el ID: {id}");
                return _operations.HandleException(ex, "NetworkTypeRepository.DeleteNetworkTypeAsync");
            }
        }

        public async Task<OperationResult> GetNetworkTypesByProviderAsync(int providerId)
        {
            try
            {
                var networkTypes = await _context.NetworkType
                    .Where(nt => nt.Id == providerId)
                    .Select(nt => new NetworkTypeModel()
                    {
                        Id = nt.Id,
                        Name = nt.Name,
                        Description = nt.Description,
                        CreatedAt = nt.CreatedAt,
                        UpdatedAt = nt.UpdatedAt,
                        IsActive = nt.IsActive,

                    }).ToListAsync();

                if (networkTypes == null || !networkTypes.Any())
                {
                    _logger.LogWarning($"No se encontró el Network con el Provider de ID: {providerId} para eliminar.");
                    return _operations.SuccessResult(null, "NetworkTypeRepository.GetNetworkTypesByProviderAsync");
                }

                return _operations.SuccessResult(networkTypes, "NetworkTypeRepository.GetNetworkTypesByProviderAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener Networ conel Provider de ID: {providerId}");
                return _operations.HandleException(ex, "NetworkTypeRepository.GetNetworkTypesByProviderAsync");
            }
        }
    }
}