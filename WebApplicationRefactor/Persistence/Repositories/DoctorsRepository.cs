using WebApplicationRefactor.Models;

using WebApplicationRefactor.Models.Doctors;
using WebApplicationRefactor.Persisten.Configuration;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Persistence.Interfaces.IRepository;
using WebApplicationRefactor.Application.Services;
using WebApplicationRefactor.Services.Interface;

namespace WebApplicationRefactor.Persisten.Repository
{
    public class DoctorsRepository : BaseApi, IRepository<DoctorsApiModel>
    {
        private readonly ILogger<DoctorsRepository> _logger;
        private readonly IErrorMessageService _errorMessageService;

        public DoctorsRepository(HttpClient httpClient, IConfiguration configuration, ILogger<DoctorsRepository> logger, IErrorMessageService errorMessageService)
            : base(httpClient, configuration)
        {
            _logger = logger;
            _errorMessageService = errorMessageService;
        }

        private OperationResult ValidateDoctorPersistence(DoctorsApiModel doctor)
        {
            if (doctor == null)
            {
                return new OperationResult
                {
                    success = false,
                    message = _errorMessageService.GetErrorMessage("EntityBase", "NullEntity")
                };
            }
            return new OperationResult { success = true };
        }

        public async Task<IEnumerable<DoctorsApiModel>> GetAllAsync()
        {
            try
            {
                return await GetAsync<List<DoctorsApiModel>>("Doctors/GetDoctors");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return new List<DoctorsApiModel>();
            }
        }

        public async Task<DoctorsApiModel> GetByIdAsync(int id)
        {
            try
            {
                return await GetAsync<DoctorsApiModel>($"Doctors/GetDoctorsByID?id={id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Generic", "GenericError"));
                return null;
            }
        }

        public async Task AddAsync(DoctorsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PostAsync("Doctors/SaveDoctor", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "SaveFailed"));
            }
        }

        public async Task UpdateAsync(DoctorsApiModel entity)
        {
            var result = ValidateDoctorPersistence(entity);
            if (!result.success)
            {
                _logger.LogError(new Exception(result.message), result.message);
                return;
            }

            try
            {
                await PutAsync($"Doctors/UpdateDoctor/{entity.Id}", entity);
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
                await DeleteAsync($"Doctors/DeleteDoctor/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _errorMessageService.GetErrorMessage("Operations", "DeleteFailed"));
            }
        }
    }
}