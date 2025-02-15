
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Model.Models;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Repositories
{
    public class AvailabilityModesRepository : BaseRepository<AvailabilityModes>, IAvailabilityModesRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<AvailabilityModesRepository> logger;
        private readonly IConfiguration configuration;

        public AvailabilityModesRepository(NetMedContext context, ILogger<AvailabilityModesRepository> logger, IConfiguration configuration)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            var result = new OperationResult();
            try
            {
                // Evitar nombres duplicados
                bool exists = await context.AvailabilityMode.AnyAsync(m => m.AvailabilityModeName == entity.AvailabilityModeName);
                if (exists)
                {
                    result.Message = "Modo ya existe";
                    result.Success = false;
                    return result;
                }

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Disponibilidad guardada";
            }
            catch (Exception ex)
            {
                result.Message = "Error al guardar el modo de disponibilidad";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetAvailabilityModeByIdAsync(int modeId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var mode = await context.AvailabilityMode
                    .Where(m => m.Id == modeId)
                    .Select(m => new AvailabilityModesModel
                    {
                        Id = m.Id,
                        AvailabilityModeName = m.AvailabilityModeName, // Ajuste para que coincida con el modelo
                    })
                    .FirstOrDefaultAsync();

                if (mode != null)
                {
                    result.Data = mode;
                    result.Success = true;
                }
                else
                {
                    result.Message = "Modo no encontrado";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorAvailabilityModesRepository:GetAvailabilityModeById"] ?? "Error al solicitar modo";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            var result = new OperationResult();
            try
            {
                var existing = await context.AvailabilityMode.FindAsync(entity.Id);
                if (existing == null)
                {
                    result.Message = "Modo no encontrado";
                    result.Success = false;
                    return result;
                }

                existing.AvailabilityModeName = entity.AvailabilityModeName;
                existing.IsActive = entity.IsActive;
                await base.UpdateEntityAsync(existing);

                result.Success = true;
                result.Message = "Modo actualizado";
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando el modo";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetByNameAsync(string availabilityModeName)
        {
            var result = new OperationResult();
            try
            {
                var mode = await context.AvailabilityMode
                    .Where(m => m.AvailabilityModeName == availabilityModeName)
                    .Select(m => new AvailabilityModesModel
                    {
                        Id = m.Id,
                        AvailabilityModeName = m.AvailabilityModeName,
                    })
                    .FirstOrDefaultAsync();

                if (mode != null)
                {
                    result.Data = mode;
                    result.Success = true;
                }
                else
                {
                    result.Message = "Modo no encontrado";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error solicitando el modo con este nombre";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> ExistsByNameAsync(string availabilityModeName)
        {
            var result = new OperationResult();
            try
            {
                bool exists = await context.AvailabilityMode.AnyAsync(m => m.AvailabilityModeName == availabilityModeName);
                result.Success = exists;
                result.Message = exists ? "El modo existe" : "El modo no existe";
            }
            catch (Exception ex)
            {
                result.Message = "Error verificando existencia de modo";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
