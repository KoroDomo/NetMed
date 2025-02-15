
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using NetMed.Persistence.Validators;

namespace NetMed.Persistence.Repositories
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<AvailabilityModesRepository> logger;

        public AvailabilityModesRepository(NetMedContext context, ILogger<AvailabilityModesRepository> logger)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
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
            var exists = await context.AvailabilityMode.AnyAsync(m => m.AvailabilityModeName == availabilityModeName);
            return new OperationResult { Success = exists, Message = exists ? "El Modo de Disponibilidad existe" : "El Modo de Disponibilidad existe no existe" };
        }

        public async Task<OperationResult> GetByNameAsync(string availabilityModeName)
        {
            var mode = await context.AvailabilityMode
                .FirstOrDefaultAsync(m => m.AvailabilityModeName == availabilityModeName);

            return mode != null
                ? new OperationResult { Success = true, Data = mode }
                : new OperationResult { Success = false, Message = "El Modo de Disponibilidad no fue encontrado" };
        }
    }
}

