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

        public NetworkTypeRepository(NetMedContext context, 
                                     ILogger<NetworkTypeRepository> logger, 
                                     IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override Task<OperationResult> SaveEntityAsync(NetworkType entity)
        {
            return base.SaveEntityAsync(entity);
        }
        public override Task<OperationResult> GetEntityByIdAsync(int id)
        {
            return base.GetEntityByIdAsync(id);
        }
        public override Task<OperationResult> UpdateEntityAsync(NetworkType entity)
        {
            return base.UpdateEntityAsync(entity);
        }
        public override Task<bool> ExistsAsync(Expression<Func<NetworkType, bool>> filter)
        {
            return base.ExistsAsync(filter);
        }
        public override Task<OperationResult> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<OperationResult> GetAllAsync(Expression<Func<NetworkType, bool>> filter)
        {
            return base.GetAllAsync(filter);
        }

        public Task<OperationResult> DeleteNetworkTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetNetworkTypesByProviderAsync(int providerId)
        {
            throw new NotImplementedException();
        }

    }
}
