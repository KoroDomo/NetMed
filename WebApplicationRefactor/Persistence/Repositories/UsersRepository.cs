using WebApplicationRefactor.Models.Users;
using WebApplicationRefactor.Persistence.Config;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace NetMed.WebApplicationRefactor.Persistence.Repositories
{
    public class UsersRepository : BaseApi, IRepository<UsersApiModel>
    {
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(HttpClient httpClient, IConfiguration configuration, ILogger<UsersRepository> logger)
            : base(httpClient, configuration)
        {
            _logger = logger;
        }

        private bool ValidateUserPersistence(UsersApiModel user)
        {
            return user != null;
        }

        public async Task<IEnumerable<UsersApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<UsersApiModel>>("Users/GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all users");
                return new List<UsersApiModel>();
            }
        }

        public async Task<UsersApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<UsersApiModel>($"Users/GetById/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by ID");
                return null;
            }
        }

        public async Task AddAsync(UsersApiModel entity)
        {
            if (!ValidateUserPersistence(entity))
            {
                _logger.LogError("Invalid user entity");
                return;
            }

            try
            {
                await PostAsync("Users", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
            }
        }

        public async Task UpdateAsync(UsersApiModel entity)
        {
            if (!ValidateUserPersistence(entity))
            {
                _logger.LogError("Invalid user entity");
                return;
            }

            try
            {
                await PutAsync($"Users/Update/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await DeleteAsync($"Users/Delete/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
            }
        }
    }
}
