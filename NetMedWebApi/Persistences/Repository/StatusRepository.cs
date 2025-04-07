using NetMedWebApi.Infrastructure.ApiClient.Base;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Infrastructure.Validator;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Status;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Persistence.Repository
{
    public class StatusRepository : ClientApi, IStatusRepository
    {
        private readonly ILoggerCustom _logger;
        private readonly IValidateGeneral _validator;
        private readonly IJsonMessage _message;

        public StatusRepository(HttpClient httpClient,
                                ILoggerCustom logger,
                                IJsonMessage message) : base(httpClient)
        {
            _logger = logger;
            _message = message;
            _validator = new ValidateGeneral(_logger, _message);
            BaseEndPoint = "Status/";
        }

        public async Task<OperationResult<SaveStatusModel>> CreateStatusAsync(SaveStatusModel model)
        {
            const string operation = "Create";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                _logger.LogInformation(_message.SuccessMessages["StatusCreated"]);
                return await PostAsync($"CreateStatus", model);

            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<DeleteStatusModel>> DeleteStatusAsync(DeleteStatusModel model)
        {
            const string operation = "Delete";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success) return result;

            try
            {
                _logger.LogInformation(_message.SuccessMessages["StatusDeleted"]);
                return await DeleteAsync($"{BaseEndPoint}/DeleteStatus", model);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
        public async Task<OperationResultList<StatusApiModel>> GetAllStatusAsync()
        {
            const string operation = "GetAll";
            var result = new OperationResultList<StatusApiModel>();

            try
            {
                result = await GetAllAsync<StatusApiModel>($"GetAllStatus");

                if (result == null || !result.Success)
                {
                    result.Message = _message.ErrorMessages["NullEntity"];
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

        public async Task<OperationResult<T>> GetStatusByIdAsync<T>(int Id)
        {
            const string operation = "Get";
            var result = _validator.CheckIfId<T>(Id, operation);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                _logger.LogInformation(_message.SuccessMessages["StatusFound"]);
                return await GetByIdAsync<T>($"GetStatusById?ID=", Id);

            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<UpdateStatusModel>> UpdateStatusAsync(UpdateStatusModel model)
        {
            const string operation = "Update";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            try
            {
                _logger.LogInformation(_message.SuccessMessages["StatusUpdated"]);
                return await PutAsync($"UpdateStatus", model);
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
