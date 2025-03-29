using Microsoft.EntityFrameworkCore;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators.Implementations;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using System.Linq.Expressions;

namespace NetMed.Persistence.Repositories
{
    public class NetworkTypeRepository : BaseRepository<NetworkType>, INetworkTypeRepository
    {
        private readonly NetMedContext _context;
        private readonly ICustomLogger _logger;
        private readonly INetworkTypeValidator _operations;

        public NetworkTypeRepository(NetMedContext context,
                                     ICustomLogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
            _operations = new NetworkTypeValidator();
        }
        public override async Task<OperationResult> SaveEntityAsync(NetworkType network)
        {
            
            try
            {
                var operationR = _operations.ValidateNameExists(network, _context);
                if (operationR.Success == false)
                {
                    _logger.LogWarning(operationR.Message);
                    return operationR;
                }

                operationR =_operations.ValidateNetworkType(network);
                if (operationR.Success == false)
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

                _logger.LogInformation(_operations.GetSuccesMessage("Networks", "RemoveNetworkType"));

                return _operations.SuccessResult(network, "Networks", "RemoveNetworkType");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Networks", "RemoveNetworkType"));

                return _operations.HandleException("Networks", "RemoveNetworkType");
            }
        }
        public async override Task<OperationResult> UpdateEntityAsync(NetworkType network)
        {
            
            try
            {
                var result = _operations.ValidateNameExists(network, _context);
                if (result.Success == false)
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
                Network.UpdatedAt = DateTime.Now;
                _context.Entry(Network).CurrentValues.SetValues(network);
                await _context.SaveChangesAsync();

                _logger.LogInformation(_operations.GetSuccesMessage("Operations", "UpdateSuccess"));
                return _operations.SuccessResult(Network, "Operations", "UpdateSuccess");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Operations", "UpdateSuccess"));
                return _operations.HandleException("Operations", "UpdateSuccess");
            }
        }
        public async Task<OperationResult> GetNetworkTypeById(int networkTypeId)
        {
            try
            {
                var networkTypes = await _context.NetworkType
                    .Where(nt => nt.Id == networkTypeId)
                    .MapToNetworkTypeModel()
                    .ToListAsync();

                if (!networkTypes.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");
                }

                return _operations.SuccessResult(networkTypes, "Networks", "GetNetworkType");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Networks", "GetNetworkType"));

                return _operations.HandleException("Networks", "GetNetworkType");
            }
        }
        public override async Task<OperationResult> GetAllAsync()
        {
            try
            {
                var network = await _context.NetworkType
                    .Where(n => n.IsActive)
                    .OrderByDescending(n => n.CreatedAt)
                    .MapToNetworkTypeModel()
                    .ToListAsync();

                if (!network.Any())
                {
                    _logger.LogWarning(_operations.GetErrorMessage("Entitys", "NotFound"));
                    return _operations.HandleException("Entitys", "NotFound");

                }
                return _operations.SuccessResult(network, "Networks", "GetNetworkType");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _operations.GetErrorMessage("Networks", "GetNetworkType"));

                return _operations.HandleException("Networks", "GetNetworkType");
            }
        }
        public override async Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
            try
            {
                var networks = await _context.NetworkType
                    .Where(filter)
                    .OrderByDescending(n => n.CreatedAt)
                    .MapToNetworkTypeModel()
                    .ToListAsync();

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