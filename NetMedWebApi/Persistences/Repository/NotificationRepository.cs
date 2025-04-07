using NetMedWebApi.Infrastructure.ApiClient.Base;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Infrastructure.Validator;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Notification;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Persistence.Repository
{
    public class NotificationRepository : ClientApi, INotificationRepository
    {
        private readonly ILoggerCustom _logger;
        private readonly IValidateGeneral _validator;
        private readonly IJsonMessage _message;

        public NotificationRepository(
            HttpClient httpClient,
            ILoggerCustom logger,
            IJsonMessage message) : base(httpClient)
        {
            _logger = logger;
            _message = message;
            _validator = new ValidateGeneral(_logger, _message);
            BaseEndPoint = "Notification/";

        }
        public async Task<OperationResultList<NotificationApiModel>> GetAllNotificationAsync()
        {
            const string operation = "GetAll";
            var result = new OperationResultList<NotificationApiModel>();

            try
            {
                result = await GetAllAsync<NotificationApiModel>($"GetAllNotifications");

                if (result == null || !result.Success)
                {
                    result.Message = _message.ErrorMessages["GetAllNull"];
                    _logger.LogWarning(result.Message);
                    return result;
                }
                result.Success = true;
                _logger.LogInformation(_message.SuccessMessages["GetAllEntity"]);
                return result;
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<UpdateNotificationModel>> UpdateNotificationAsync(UpdateNotificationModel model)
        {
            const string operation = "Update";
            var result = _validator.CheckIfEntityIsNull(model, operation);


            try
            {
                _logger.LogInformation(_message.SuccessMessages["NotificationUpdated"]);
                return await PutAsync($"UpdateNotifications", model);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<T>> GetNotificationByIdAsync<T>(int id)
        {
            const string operation = "Get";
            var result = _validator.CheckIfId<T>(id, operation);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                _logger.LogInformation(_message.SuccessMessages["NotificationFound"]);
                return await GetByIdAsync<T>($"GetById?Id=", id);

            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        

        public async Task<OperationResult<SaveNotificationModel>> CreateNotificationAsync(SaveNotificationModel model)
        {
            const string operation = "Create";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success)
            {
                return result;
            } 

            try
            {
                _logger.LogInformation(_message.SuccessMessages["NotificationCreated"]);
                return await PostAsync($"CreatedNotifications", model);

          



            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

   

        public async Task<OperationResult<DeleteNotificationModel>> DeleteNotificationAsync(DeleteNotificationModel model)
        {
            const string operation = "Delete";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success) return result;

            try
            {
                _logger.LogInformation(_message.SuccessMessages["NotificationDeleted"]);
                return await DeleteAsync($"{BaseEndPoint}/DeleteNotifications", model);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}