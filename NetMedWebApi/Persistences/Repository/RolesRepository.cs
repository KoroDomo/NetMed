using NetMed.Domain.Entities;
using NetMedWebApi.Infrastructure.ApiClient.Base;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.Infrastructure.Validator;
using NetMedWebApi.Models;
using NetMedWebApi.Persistence.Interfaces;

namespace NetMedWebApi.Persistences.Repository
{
    public class RolesRepository : ClientApi, IRolesRepository
    {
        private readonly ILoggerCustom _logger;
        private readonly IValidateGeneral _validator;
        private readonly IJsonMessage _message;
        private readonly string _baseEndPoint = "Roles/";

        public RolesRepository(
            HttpClient httpClient,
            ILoggerCustom logger,
            IJsonMessage message) : base(httpClient)
        {
            _logger = logger;
            _message = message;
            _validator = new ValidateGeneral(_logger, _message);
            BaseEndPoint = _baseEndPoint;
        }

        public async Task<NetMed.Domain.Base.OperationResult> GetRoleByIdAsync(int roles)
        {
            const string operation = "Get";
            var result = _validator.CheckIfId<Roles>(roles, operation);

            if (!result.Success)
                return result;

            try
            {
                return await GetByIdAsync<Roles>($"GetRolesById?roles=", roles);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["DatabaseError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult> CreateRoleAsync(Roles roles)
        {
            const string operation = "Create";
            var result = _validator.CheckIfEntityIsNull(roles, operation);

            if (!result.Success)
                return result;

            try
            {
                return await PostAsync("CreateRole", roles);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Roles roles)
        {
            const string operation = "Update";
            var result = _validator.CheckIfEntityIsNull(roles, operation);

            if (!result.Success)
                return result;

            try
            {
                return await PutAsync("UpdateRole", roles);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int roles)
        {
            const string operation = "Delete";
            var result = _validator.CheckIfId<Roles>(roles, operation);

            if (!result.Success)
                return result;

            try
            {
                return await DeleteAsync($"{_baseEndPoint}DeleteRole", roles);
            }
            catch (Exception ex)
            {
                result.Message = _message.ErrorMessages["GeneralError"];
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<OperationResultList<Roles>> GetAllRolesAsync()
        {
            var result = new OperationResultList<Roles>();

            try
            {
                result = await GetAllAsync<Roles>("GetAllRoles");

                if (result == null || !result.Success)
                {
                    result.Message = _message.ErrorMessages["GetAllNull"];
                    _logger.LogWarning(result.Message);
                }

                return result;
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
