using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
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
        private readonly OperationValidator _operations= new OperationValidator();

        public NetworkTypeRepository(NetMedContext context,
                                     ILogger<NetworkTypeRepository> logger,
                                     IConfiguration configuration) : base(context, logger,configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            
        }

        public override async Task<OperationResult> SaveEntityAsync(NetworkType entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null,_configuration, "La entidad no puede ser nula.");
                }

                _context.NetworkTypes.Add(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity,_configuration, "NetworkTypeRepository.SaveEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.SaveEntityAsync", _configuration);
            }
        }

        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            try
            {
                var entity = await _context.NetworkTypes.FindAsync(id);
                if (entity == null)
                {
                    return _operations.SuccessResult(null,_configuration, "NetworkTypeRepository.GetEntityByIdAsync");
                }

                return _operations.SuccessResult(entity, _configuration, "NetworkTypeRepository.GetEntityByIdAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.GetEntityByIdAsync", _configuration);
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(NetworkType entity)
        {
            try
            {
                if (entity == null)
                {
                    return _operations.SuccessResult(null, _configuration, "La entidad no puede ser nula.");
                }

                _context.NetworkTypes.Update(entity);
                await _context.SaveChangesAsync();

                return _operations.SuccessResult(entity, _configuration, "NetworkTypeRepository.UpdateEntityAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.UpdateEntityAsync", _configuration);
            }
        }

        public override async Task<bool> ExistsAsync(Expression<Func<NetworkType, bool>> filter)
        {
            try
            {
                return await _context.NetworkTypes.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar la existencia de la entidad con el filtro: {Filter}", filter.ToString());
                throw; 
            }
        }

        public override async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var entities = await _context.NetworkTypes.ToListAsync();
                if (entities == null || !entities.Any())
                {
                    return _operations.SuccessResult(null, _configuration, "NetworkTypeRepository.GetAllAsync");
                }

                return _operations.SuccessResult(entities, _configuration, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync", _configuration);
            }
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
            try
            {
                var entities = await _context.NetworkTypes
                    .Where(filter)
                    .ToListAsync();

                if (entities == null || !entities.Any())
                {
                    return _operations.SuccessResult(null, _configuration, "NetworkTypeRepository.GetAllAsync");
                }

                return _operations.SuccessResult(entities, _configuration, "NetworkTypeRepository.GetAllAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.GetAllAsync", _configuration);
            }
        }

        public async Task<OperationResult> DeleteNetworkTypeAsync(int id)
        {
            try
            {
                var entity = await _context.NetworkTypes.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("No se encontró la entidad con el ID: {Id} para eliminar.", id);
                    return _operations.SuccessResult(null, _configuration, "Entidad no encontrada.");
                }

                _context.NetworkTypes.Remove(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Entidad eliminada exitosamente: {Entity}", entity.ToString());
                return _operations.SuccessResult(null, _configuration, "Entidad eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la entidad con el ID: {Id}", id);
                return _operations.HandleException(ex, "Ocurrió un error eliminando la entidad", _configuration);
            }
        }

        public async Task<OperationResult> GetNetworkTypesByProviderAsync(int providerId)
        {
            try
            {
                var networkTypes = await _context.NetworkTypes
                    .Where(nt => nt.Id == providerId)
                    .ToListAsync();

                if (networkTypes == null || !networkTypes.Any())
                {
                    return _operations.SuccessResult(null, _configuration, "NetworkTypeRepository.GetNetworkTypesByProviderAsync");
                }

                return _operations.SuccessResult(networkTypes, _configuration, "NetworkTypeRepository.GetNetworkTypesByProviderAsync");
            }
            catch (Exception ex)
            {
                return _operations.HandleException(ex, "NetworkTypeRepository.GetNetworkTypesByProviderAsync", _configuration);
            }
        }
    }
}