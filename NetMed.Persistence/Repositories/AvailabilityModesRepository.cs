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
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "El Modo de Disponibilidad no puede ser nulo";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.AvailabilityMode))
                {
                    result.Success = false;
                    result.Message = "El nombre del Modo de Disponibilidad no puede ser nulo";
                    return result;
                }
                if (await base.Exists(amode => amode.AvailabilityMode == entity.AvailabilityMode ))
                {
                    result.Success = false;
                    result.Message = "El Modo de Disponibilidad ya existe";
                    return result;
                }
                await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
        public override async Task<OperationResult> UpdateEntityAsync(AvailabilityModes entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "El Modo de Disponibilidad no puede ser nulo";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.AvailabilityMode))
                {
                    result.Success = false;
                    result.Message = "El nombre del Modo de Disponibilidad no puede ser nulo";
                    return result;
                }
                if (entity.AvailabilityMode.Length > 15)
                {
                    result.Success = false;
                    result.Message = "El nombre del Modo de Disponibilidad no puede tener mas de 15 caracteres";
                    return result;
                }
                if (await base.Exists(amode => amode.AvailabilityMode == entity.AvailabilityMode))
                {
                    result.Success = false;
                    result.Message = "El Modo de Disponibilidad ya existe";
                    return result;
                }
                await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetByNameAsync(string availabilityModeName)
        {
            OperationResult result = new OperationResult();
            try
            {
                var availabilityMode = await _context.AvailabilityModes.FirstOrDefaultAsync
                    (amode => amode.AvailabilityMode == availabilityModeName);
                if (availabilityMode == null)
                {
                    result.Success = false;
                    result.Message = "El Modo de Disponibilidad no existe";
                    return result;
                }
                result.Data = availabilityMode;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}