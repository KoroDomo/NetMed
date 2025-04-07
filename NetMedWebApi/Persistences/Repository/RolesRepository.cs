using NetMedWebApi.Infrastructure.ApiClient.Base;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Infrastructure.Validator;
using NetMedWebApi.Models;
using NetMedWebApi.Models.Roles;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Persistence.Repository
{
    public class RolesRepository : ClientApi, IRolesRepository
    {
        private readonly ILoggerCustom _logger;
        private readonly IValidateGeneral _validator;
        private readonly IJsonMessage _message;

        public RolesRepository(
            HttpClient httpClient,
            ILoggerCustom logger,
            IJsonMessage message) : base(httpClient)
        {
            _logger = logger;
            _message = message;
            _validator = new ValidateGeneral(_logger, _message);
            BaseEndPoint = "Roles/";
        }

        public async Task<OperationResultList<RolesApiModel>> GetAllRolesAsync()
        {
            const string operation = "GetAll";
            var result = new OperationResultList<RolesApiModel>();

            try
            {
                result = await GetAllAsync<RolesApiModel>($"GetAllRoles");

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

        public async Task<OperationResult<UpdateRolesModel>> UpdateRoleAsync(UpdateRolesModel model)
        {
            const string operation = "Update";
            var result = _validator.CheckIfEntityIsNull(model, operation);


            try
            {
                _logger.LogInformation(_message.SuccessMessages["RoleUpdated"]);
                return await PutAsync($"UpdateRole", model);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }


        public async Task<OperationResult<T>> GetRoleByIdAsync<T>(int Id)
        {
            const string operation = "Get";
            var result = _validator.CheckIfId<T>(Id, operation);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                _logger.LogInformation(_message.SuccessMessages["RoleFound"]);
                return await GetByIdAsync<T>($"GetRolesById?roles=", Id);

            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<SaveRolesModel>> CreateRoleAsync(SaveRolesModel model)
        {
            const string operation = "Create";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success)
            {
                return result;
            }

            try
            {
                _logger.LogInformation(_message.SuccessMessages["RoleCreated"]);
                return await PostAsync($"CreateRole", model);

            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult<DeleteRolesModel>> DeleteRoleAsync(DeleteRolesModel model)
        {
            const string operation = "Delete";
            var result = _validator.CheckIfEntityIsNull(model, operation);

            if (!result.Success) return result;

            try
            {
                _logger.LogInformation(_message.SuccessMessages["RoleDeleted"]);
                return await DeleteAsync($"DeleteRole", model);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

    }
}
