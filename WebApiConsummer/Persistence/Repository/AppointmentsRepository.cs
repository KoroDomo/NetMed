using NetMed.WebApi.Models.Appointments;
using NetMed.WebApi.Models.OperationsResult;
using WebApiApplication.Persistence.Interfaces;
using WebApiApplication.Infraestructura.Messages;
using WebApiApplication.Infraestructura.Logger;
using WebApiApplication.Models.OperationsResult;
using WebApiApplication.Persistence.Base;

namespace WebApiApplication.Persistence.Repository
{
    public class AppointmentRepository
    : BaseRepository<AppointmentsModel, AppointmentsModelSave, AppointmentsModelUpdate, AppointmentsModelRemove>, IAppointmentsRepository
      
    {
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public AppointmentRepository(HttpClient client, IMessageService messageService, ILoggerSystem logger) : base(client)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<List<AppointmentsModel>> GetAllAppointmentsAsync()
        {
            var result = new OperationResultTypeList<AppointmentsModel>();
            try
            {
                return await GetAllAsync("Appointments/GetAppointment");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }            
        public async Task<AppointmentsModel> GetAppointmentByIdAsync(int id)
        {
            var result = new OperationResultType<AppointmentsModel>();
            try
            {
                return await GetByIdAsync($"Appointments/GetAppointmentById?id={id}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetByIdAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }          
        }         
        public async Task<bool> CreateAppointmentAsync(AppointmentsModelSave model)
        {
            var result = new OperationResultType<bool>();
            try
            {
                return await SaveAsync("Appointments/SaveAppointement", model);
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(SaveAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }
        public async Task<bool> UpdateAppointmentAsync(AppointmentsModelUpdate model)
        {
            var result = new OperationResultType<bool>();
            try
            {
                return await UpdateAsync("Appointments/UpdateAppointment", model);
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(UpdateAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }           
        public async Task<bool> DeleteAppointmentAsync(AppointmentsModelRemove model)
        {
            var result = new OperationResultType<bool>();
            try
            {
               return await RemoveAsync("Appointments/RemoveAppointment", model);
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(RemoveAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }           
    }
}

