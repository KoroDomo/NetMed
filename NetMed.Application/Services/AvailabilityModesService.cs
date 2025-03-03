using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.AvailabilityModes;
using NetMed.Application.Dtos.Specialties;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;


namespace NetMed.Application.Services
{
    public class AvailabilityModesService : IAvailabilityModesService
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;
        private readonly ILogger<AvailabilityModesService> _logger;
        private readonly IConfiguration _configuration;

        public AvailabilityModesService(IAvailabilityModesRepository availabilityModesRepository,
                                        ILogger<AvailabilityModesService> logger,
                                        IConfiguration configuration) 
        {
            _availabilityModesRepository = availabilityModesRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var aModes = await _availabilityModesRepository.GetAll();

                if (!aModes.Success)
                {
                    result.Success = false;
                    result.Message = aModes.Message;
                    return result;
                }
                result.Data = aModes.Data;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo los Modos de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var aModes = await _availabilityModesRepository.GetEntityByIdAsync(Id);
                if (!aModes.Success)
                {
                    result.Success = false;
                    result.Message = aModes.Message;
                    return result;
                }
                result.Data = aModes.Data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public Task<OperationResult> Remove(RemoveAvailabilityModesDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Save(SaveAvailabilityModesDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                AvailabilityModes aMode = new AvailabilityModes()
                {
                    AvailabilityMode = dto.AvailabilityMode


                };

                var aM = await _availabilityModesRepository.SaveEntityAsync(aMode);

                result.Success = aM.Success;
                result.Message = aM.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error guardando el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateAvailabilityModesDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var resultGetById = await _availabilityModesRepository.GetEntityByIdAsync(dto.SAvailabilityModeID);

                if (!resultGetById.Success)
                {
                    result.Success = resultGetById.Success;
                    result.Message = resultGetById.Message;
                    return result;
                }

                AvailabilityModes? aMode = new AvailabilityModes()
                {
                    Id = (short)dto.SAvailabilityModeID,
                    AvailabilityMode = dto.AvailabilityMode

                };
                var aM = await _availabilityModesRepository.UpdateEntityAsync(aMode);
                result.Success = aM.Success;
                result.Message = aM.Message;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error actualizando el Modo de Disponibilidad";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
