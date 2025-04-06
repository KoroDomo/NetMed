using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Persistence.Config;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace NetMed.WebApplicationRefactor.Persistence.Repositories
{
    public class DoctorsRepository : BaseApi, IRepository<DoctorsApiModel>
    {
        private readonly ILogger<DoctorsRepository> _logger;

        public DoctorsRepository(HttpClient httpClient, IConfiguration configuration, ILogger<DoctorsRepository> logger)
            : base(httpClient, configuration)
        {
            _logger = logger;
        }

        private OperationResult ValidateDoctorPersistence(DoctorsApiModel doctor)
        {
            if (doctor == null)
            {
                return new OperationResult
                {
                    Success = false,
                };
            }
            return new OperationResult { Success = true };
        }

        public async Task<IEnumerable<DoctorsApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<DoctorsApiModel>>("Doctors/GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "GenericError");
                return new List<DoctorsApiModel>();
            }
        }

        public async Task<DoctorsApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<DoctorsApiModel>($"Doctor/GetById/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "GenericError");
                return null;
            }
        }

        public async Task AddAsync(DoctorsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.Success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PostAsync("Doctor", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "SaveFailed");
            }
        }

        public async Task UpdateAsync(DoctorsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.Success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PutAsync($"Doctor/Update/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "UpdateFailed");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await DeleteAsync($"Doctor/Delete/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "DeleteFailed");
            }
        }
    }
}
