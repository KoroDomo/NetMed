using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.Persistence.Config;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace NetMed.WebApplicationRefactor.Persistence.Repositories
{
    public class PatientsRepository : BaseApi, IRepository<PatientsApiModel>
    {
        private readonly ILogger<PatientsRepository> _logger;

        public PatientsRepository(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<PatientsRepository> logger) : base(httpClient, configuration)
        {
            _logger = logger;
        }

        private OperationResult ValidateDoctorPersistence(PatientsApiModel patientsApiModel)
        {
            if (patientsApiModel == null)
            {
                return new OperationResult
                {
                    Success = false,
                };
            }
            return new OperationResult { Success = true };
        }
        public async Task<PatientsApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<PatientsApiModel>($"Patients/GetById/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patient by ID");
                return null;
            }
        }

        public async Task<IEnumerable<PatientsApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<PatientsApiModel>>("Patients/GetAll");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all patients");
                return new List<PatientsApiModel>();
            }
        }

        public async Task AddAsync(PatientsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.Success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }
            try
            {
                await PostAsync("Patients", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding patient");
       
            }
        }

        public async Task UpdateAsync(PatientsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.Success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }
            try
            {
              await PutAsync($"Patients/Update/{entity.Id}", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient");
           
            }
        }

        public async Task DeleteAsync(int id)
        {

            try
            {
                await DeleteAsync($"Patients/Delete/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient");
                
            }

        }
    }




}

