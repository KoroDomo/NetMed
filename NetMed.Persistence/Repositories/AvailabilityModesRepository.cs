
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

        public AvailabilityModesRepository(NetMedContext context, ILogger<AvailabilityModesRepository> logger, IConfiguration configuration) : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        public override Task<OperationResult> SaveEntityAsync(AvailabilityModes entity)
        {
            // Agregar validaciones correspondientes antes de guardar la entidad
            return base.SaveEntityAsync(entity);
        }

        public async Task<OperationResult> GetAvailabilityModeByIdAsync(int modeId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var mode = await context.AvailabilityMode
                    .Where(m => m.Id == modeId)
                    .Select(m => new AvailabilityModesModel()
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
                    result.Message = "Availability mode not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorAvailabilityModesRepository:GetAvailabilityModeById"] ?? "An error occurred while retrieving the availability mode.";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            // Agregar validaciones correspondientes antes de actualizar la entidad
            return base.UpdateEntityAsync(entity);
        }

        public Task<OperationResult> AvailabilityModeName(string AvailabilityModeName)
        {
            throw new NotImplementedException();
        }
    }
}
