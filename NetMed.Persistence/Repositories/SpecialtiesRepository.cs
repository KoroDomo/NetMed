
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Persistence;

namespace NetMed.Persistence.Repositories
{
    public class SpecialtiesRepository : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<SpecialtiesRepository> logger;

        public SpecialtiesRepository(NetMedContext context, ILogger<SpecialtiesRepository> logger)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Especialidad");
            if (!validation.Success) return validation;

            await base.SaveEntityAsync(entity);
            return new OperationResult { Success = true, Message = "Especialidad guardada" };
        }

        public override async Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Especialidad");
            if (!validation.Success) return validation;

            var existingSpecialty = await context.Specialties.FindAsync(entity.Id);
            if (existingSpecialty == null)
            {
                return new OperationResult { Success = false, Message = "Especialidad no encontrada" };
            }

            existingSpecialty.SpecialtyName = entity.SpecialtyName;
            existingSpecialty.IsActive = entity.IsActive;

            await base.UpdateEntityAsync(existingSpecialty);
            return new OperationResult { Success = true, Message = "Especialidad actualizada" };
        }

        public async Task<OperationResult> ExistsByNameAsync(string specialtyName)
        {
            var exists = await context.Specialties.AnyAsync(s => s.SpecialtyName == specialtyName);
            return new OperationResult { Success = exists, Message = exists ? "La Especialidad existe" : "Especialidad no existe" };
        }

        public async Task<OperationResult> GetByNameAsync(string specialtyName)
        {
            var specialty = await context.Specialties
                .Where(s => s.SpecialtyName == specialtyName)
                .FirstOrDefaultAsync();

            return specialty != null
                ? new OperationResult { Success = true, Data = specialty }
                : new OperationResult { Success = false, Message = "Especialidad no encontrada" };
        }
    }
}
