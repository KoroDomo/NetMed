using NetMed.WebApi.Models.DoctorAvailability;
using NetMed.WebApi.Models.OperationsResult;
using WebApiApplication.Infraestructura.Logger;
using WebApiApplication.Infraestructura.Messages;
using WebApiApplication.Models.OperationsResult;
using WebApiApplication.Persistence.Base;
using WebApiApplication.Persistence.Interfaces;


namespace WebApiApplication.Persistence.Repository
{
    public class DoctorAvailabilityRepository : BaseRepository<DoctorAvailabilityModel, DoctorAvailabilityModelSave, DoctorAvailabilityModelUpdate, DoctorAvailabilityModelRemove>, IDoctorAvailabilityRepository
    {
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public DoctorAvailabilityRepository(HttpClient httpClient, IMessageService messageService, ILoggerSystem logger) : base(httpClient)
        {
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<List<DoctorAvailabilityModel>> GetAllDoctorAvailabilityAsync()
        {
            var result = new OperationResultTypeList<DoctorAvailabilityModel>();
            try
            {
                return await GetAllAsync("DoctorAvailability/GetDoctorAvailability");
            }
            catch (Exception ex )
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetAllAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }
        public async Task<DoctorAvailabilityModel> GetDoctorAvailabilityByIdAsync(int id)
        {
            var result = new OperationResultType<DoctorAvailabilityModel>();    
            try
            {
                return await GetByIdAsync($"DoctorAvailability/GetAvailabilityById?id={id}");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(GetByIdAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }    
        public async Task<bool> CreateDoctorAvailabilityAsync(DoctorAvailabilityModelSave model)
        {
            var result = new OperationResultType<bool>();
            try
            {
                 return await SaveAsync("DoctorAvailability/SaveDoctorAvailability", model);
            }
            catch (Exception ex )
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(SaveAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }
        public async Task<bool> UpdateDoctorAvailabilityAsync(DoctorAvailabilityModelUpdate model)
        {
            var result = new OperationResultType<bool>();
            try
            {
                return await UpdateAsync("DoctorAvailability/UpdateDoctorAvailability", model);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = _messageService.GetMessage(nameof(UpdateAsync), false);
                _logger.LogError(ex, result.Message);
                return result.data;
            }
        }
        public async Task<bool> DeleteDoctorAvailabilityAsync(DoctorAvailabilityModelRemove model)
        {
            var result = new OperationResultType<bool>();
            try
            {
                 return await RemoveAsync("DoctorAvailability/RemoveDoctorAvailability", model);
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

