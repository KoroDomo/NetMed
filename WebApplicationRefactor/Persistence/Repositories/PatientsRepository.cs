using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Persisten.Configuration;
using WebApplicationRefactor.Services.Interface;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;

namespace WebApplicationRefactor.Persisten.Repository
{
    public class PatientsRepository : BaseApi, IRepository<PatientsApiModel>
    {
        private readonly ILogger<PatientsRepository> _logger;
        private readonly IErrorMessageService _errorMessageService;

        public PatientsRepository(HttpClient httpClient, IConfiguration configuration, ILogger<PatientsRepository> logger, IErrorMessageService errorMessageService)
            : base(httpClient, configuration)
        {
            _logger = logger;
            _errorMessageService = errorMessageService;
        }

        private OperationResult ValidatePatientPersistence(PatientsApiModel patient)
        {
            if (patient == null)
            {
                return new OperationResult
                {
                    success = false,
                    message = _errorMessageService.GetErrorMessage("EntityBase", "NullEntity")
                };
            }
            return new OperationResult { success = true };
        }

        public async Task<IEnumerable<PatientsApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<PatientsApiModel>>("Patients/GetPatients");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return new List<PatientsApiModel>();
            }
        }

        public async Task<PatientsApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<PatientsApiModel>($"Patients/GetPatientsByID?id={id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return null;
            }
        }

        public async Task AddAsync(PatientsApiModel entity)
        {
            var result = ValidatePatientPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PostAsync("Patients/SavePatient", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "SaveFailed"));
            }
        }

        public async Task UpdateAsync(PatientsApiModel entity)
        {
            var result = ValidatePatientPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PutAsync($"Patients/UpdatePatient/{entity.Id}", entity);
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
                await DeleteAsync($"Patients/DeletePatient/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "DeleteFailed"));
            }
        }
    }
}