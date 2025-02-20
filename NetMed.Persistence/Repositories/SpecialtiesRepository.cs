
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Persistence;

namespace NetMed.Persistence.Repositories
{
    public class SpecialtiesRepository : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<SpecialtiesRepository> _logger;
        private readonly IConfiguration _configuration;

        public SpecialtiesRepository(NetMedContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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

            var existingSpecialty = await _context.Specialties.FindAsync(entity.Id);
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
            var exists = await _context.Specialties.AnyAsync(s => s.SpecialtyName == specialtyName);
            return new OperationResult { Success = exists, Message = exists ? "La Especialidad existe" : "Especialidad no existe" };
        }

        public async Task<OperationResult> GetByNameAsync(string specialtyName)
        {
            var specialty = await _context.Specialties
                .Where(s => s.SpecialtyName == specialtyName)
                .FirstOrDefaultAsync();

            return specialty != null
                ? new OperationResult { Success = true, Data = specialty }
                : new OperationResult { Success = false, Message = "Especialidad no encontrada" };
        }
    }
}
