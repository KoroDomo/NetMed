using Microsoft.EntityFrameworkCore;
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
                                     MessageMapper messageMapper) : base(context, logger, messageMapper)
        {
            _context = context;
            _logger = logger;
            _operations = new NetworkTypeValidator(messageMapper);
        }
        public override async Task<OperationResult> SaveEntityAsync(NetworkType network)
        {
            
            try
            {
                var operationR = _operations.ValidateNameExists(network, _context);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                operationR =_operations.ValidateNetworkType(network);
                if (!operationR.Success)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                _context.NetworkType.Add(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Operations", "SaveSuccess"));

                return _operations.SuccessResult(network, "Operations", "SaveSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));

                return _operations.HandleException("Operations", "SaveFailed");

            }
        }
        public async Task<OperationResult> RemoveNetworkTypeAsync(int id)
        {
            try
            {
                var network = await _context.NetworkType.FindAsync(id);

                if (network == null)
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                network.IsActive = false;

                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Insurances", "RemoveInsurenProvider"));

                return _operations.SuccessResult(network, "Insurances", "RemoveInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "RemoveInsurenProvider"));

                return _operations.HandleException("Insurances", "RemoveInsurenProvider");
            }
        }
        public async override Task<OperationResult> UpdateEntityAsync(NetworkType network)
        {
            
            try
            {
                var result = _operations.ValidateNameExists(network, _context);
                if (!result.Success)
                {
                    _logger.LogWarning(result.Message);
                    return result;
                }

                var Network = await _context.NetworkType.FindAsync(network.Id);
                
                if (Network == null)
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }
                _context.Entry(Network).CurrentValues.SetValues(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Operations", "SaveSuccess"));
                return _operations.SuccessResult(Network, "Operations", "SaveSuccess");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "SaveFailed"));
                return _operations.HandleException("Operations", "SaveFailed");
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
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(networkTypes, "Insurances", "GetInsurenProvider");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");
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
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");

                }
                return _operations.SuccessResult(network, "Insurances", "GetInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");
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
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(networks, "Insurances", "GetInsurenProvider");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Insurances", "GetInsurenProvider"));

                return _operations.HandleException("Insurances", "GetInsurenProvider");
            }
        }
    }
}