using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.Extensions.Configuration;

namespace NetMed.Persistence.Repositories
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<AvailabilityModesRepository> _logger;
        private readonly IConfiguration _configuration;
        public AvailabilityModesRepository(NetMedContext context, ILogger<AvailabilityModesRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Modo de Disponibilidad");
            if (!validation.Success) return validation;

            await base.SaveEntityAsync(entity);
            return new OperationResult { Success = true, Message = "Modo de Disponibilidad guardado" };
        }
        public override async Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Modo de Disponibilidad");
            if (!validation.Success) return validation;

            await base.UpdateEntityAsync(entity);
            return new OperationResult { Success = true, Message = "Modo de Disponibilidad actualizado" };
        }
        public async Task<OperationResult> ExistsByNameAsync(string availabilityModeName)
        {
            if (string.IsNullOrWhiteSpace(availabilityModeName))
            {
                return new OperationResult { Success = false, Message = "El nombre del Modo de Disponibilidad esta vacio" };
            }

            var exists = await _context.AvailabilityModes.AnyAsync(m => m.AvailabilityMode == availabilityModeName);
            return new OperationResult { Success = exists, Message = exists ? "El Modo de Disponibilidad existe" : "El Modo de Disponibilidad no existe" };
        }
        public async Task<OperationResult> GetByNameAsync(string availabilityModeName)
        {
            var mode = await _context.AvailabilityModes
                .FirstOrDefaultAsync(m => m.AvailabilityMode == availabilityModeName);

            return mode != null
                ? new OperationResult { Success = true, Data = mode }
                : new OperationResult { Success = false, Message = "El Modo de Disponibilidad no fue encontrado" };
        }
    }
}