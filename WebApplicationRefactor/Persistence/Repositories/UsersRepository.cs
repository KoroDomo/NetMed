using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Users;
using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Services.Interface;
using WebApplicationRefactor.Persisten.Configuration;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace WebApplicationRefactor.Persisten.Repository
{
    public class UsersRepository : BaseApi, IRepository<UsersApiModel>
    {
        private readonly ILogger<UsersRepository> _logger;
        private readonly IErrorMessageService _errorMessageService;

        public UsersRepository(HttpClient httpClient, IConfiguration configuration, ILogger<UsersRepository> logger, IErrorMessageService errorMessageService)
            : base(httpClient, configuration)
        {
            _logger = logger;
            _errorMessageService = errorMessageService;
        }

        private OperationResult ValidateUserPersistence(UsersApiModel user)
        {
            if (user == null)
            {
                return new OperationResult
                {
                    success = false,
                    message = _errorMessageService.GetErrorMessage("EntityBase", "NullEntity")
                };
            }
            return new OperationResult { success = true };
        }

        public async Task<IEnumerable<UsersApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<UsersApiModel>>("Users/GetUsers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return new List<UsersApiModel>();
            }
        }

        public async Task<UsersApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<UsersApiModel>($"Users/GetUsersByID?id={id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return null;
            }
        }

        public async Task AddAsync(UsersApiModel entity)
        {
            var result = ValidateUserPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PostAsync("Users/SaveUser", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "SaveFailed"));
            }
        }

        public async Task UpdateAsync(UsersApiModel entity)
        {
            var result = ValidateUserPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PutAsync($"Users/UpdateUser/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "UpdateFailed"));
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await DeleteAsync($"Users/DeleteUser/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "DeleteFailed"));
            }
        }
    }
}