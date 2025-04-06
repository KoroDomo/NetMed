



using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Core.IRepository;
using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Persistence.Config;

namespace NetMed.WebApplicationRefactor.Persistence.Repositories
{


    public class DoctorsRepository : BaseApi, IRepository<DoctorsApiModel>
    {
        private readonly ILogger<DoctorsRepository> _logger;


        public DoctorsRepository(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<DoctorsRepository> logger) : base(httpClient, configuration)
        {
            _logger = logger;

        }

        public async Task AddAsync(DoctorsApiModel entity)
        {
            try
            {
                await PostAsync("Doctor", entity);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding doctor");
                throw;
            }
        }

        public Task DeleteAsync(int id)
        {
            try
            {
                return DeleteAsync($"Doctor/Delete/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor");
                throw;
            }
        }
        public async Task<IEnumerable<DoctorsApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<DoctorsApiModel>>("Doctors/GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all doctors");
                throw;
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
                _logger.LogError(ex, "Error fetching doctor by ID");
                throw;
            }
        }

        public Task UpdateAsync(DoctorsApiModel entity)
        {
            try
            {
                return PutAsync($"Doctor/Update/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor");
                throw;
            }


        }
    }

}