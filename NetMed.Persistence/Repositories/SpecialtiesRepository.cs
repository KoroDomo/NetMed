
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Model.Models;


namespace NetMed.Persistence.Repositories
{
    public class SpecialtiesRepository : BaseRepository<Specialties>, ISpecialtiesRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<SpecialtiesRepository> logger;
        private readonly IConfiguration configuration;

        public SpecialtiesRepository(NetMedContext context, ILogger<SpecialtiesRepository> logger, IConfiguration configuration)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<OperationResult> ExistsByNameAsync(string specialtyName)
        {
            var result = new OperationResult();
            try
            {
                bool exists = await context.Specialties.AnyAsync(s => s.SpecialtyName == specialtyName);
                result.Success = exists;
                result.Message = exists ? "La especialidad existe" : "Esta especialidad no existe";
            }
            catch (Exception ex)
            {
                result.Message = "Error al verificar existencia de Especialidad";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetByNameAsync(string specialtyName)
        {
            var result = new OperationResult();
            try
            {
                var specialty = await context.Specialties
                    .Where(s => s.SpecialtyName == specialtyName)
                    .Select(s => new SpecialtiesModel
                    {
                        Id = s.Id,
                        SpecialtyName = s.SpecialtyName
                    })
                    .FirstOrDefaultAsync();

                if (specialty != null)
                {
                    result.Data = specialty;
                    result.Success = true;
                }
                else
                {
                    result.Message = "Especialidad no encontrada";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error solicitando Especialidad con este nombre";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(Specialties entity)
        {
            var result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = this.configuration["Messages:ErrorSpecialtiesRepository:NullEntity"] ?? "La Especialidad es nula";
                    return result;
                }

                // Validar si la especialidad ya existe
                bool exists = await context.Specialties.AnyAsync(s => s.SpecialtyName == entity.SpecialtyName);
                if (exists)
                {
                    result.Success = false;
                    result.Message = "La Especialidad ya existe";
                    return result;
                }

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Especialidad guardada";
            }
            catch (Exception ex)
            {
                result.Message = "Error guardando Especialidad";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> SpecialtyName(string specialtyName)
        {
            var result = new OperationResult();
            try
            {
                var query = await context.Specialties
                    .Where(s => s.SpecialtyName == specialtyName)
                    .Select(s => new SpecialtiesModel
                    {
                        Id = s.Id,
                        SpecialtyName = s.SpecialtyName
                    })
                    .ToListAsync();

                if (query.Any())
                {
                    result.Data = query;
                    result.Success = true;
                }
                else
                {
                    result.Message = "No se enconto una Especialidad";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorSpecialtiesRepository:SpecialtyName"] ?? "Error solicitando la Especialidad";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Specialties entity)
        {
            var result = new OperationResult();
            try
            {
                var existingSpecialty = await context.Specialties.FindAsync(entity.Id);
                if (existingSpecialty == null)
                {
                    result.Message = "Especialidad no encontrada";
                    result.Success = false;
                    return result;
                }

                existingSpecialty.SpecialtyName = entity.SpecialtyName;
                existingSpecialty.IsActive = entity.IsActive;
                existingSpecialty.DateOfVisit = entity.DateOfVisit;

                await base.UpdateEntityAsync(existingSpecialty);
                result.Success = true;
                result.Message = "Especialidad actualizada";
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Especialidad";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}