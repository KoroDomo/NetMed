
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRespository _appointmentsRespository;
        private readonly ILogger<AppointmentsService> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsService(IAppointmentsRespository appointmentsRespository,                     ILogger<AppointmentsService> logger, IConfiguration configuration)
        {
            _appointmentsRespository = appointmentsRespository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var appointments = await _appointmentsRespository.GetAllAsync();
                
            }
            catch (Exception ex )
            {
                operationResult.Message = _configuration["AppointmentsServiceError: GetAll"];
                operationResult.Success = false;
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult> GetById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var appointments = await _appointmentsRespository.GetEntityByIdAsync(Id);
            }
            catch (Exception ex)
            {
                operationResult.Message = _configuration["AppointmentsServiceError: GetById"];
                operationResult.Success = false;
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
        public async Task<OperationResult> Remove(RemoveAppointmentsDto TDto)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var appointments = await _appointmentsRespository.RemoveAsync();
            }
            catch (Exception ex)
            {
                operationResult.Message = _configuration["AppointmentsServiceError: GetById"];
                operationResult.Success = false;
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public Task<OperationResult> Save(SaveAppointmentsDto TDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateAppointmentsDto TDto)
        {
            throw new NotImplementedException();
        }
    }
}
