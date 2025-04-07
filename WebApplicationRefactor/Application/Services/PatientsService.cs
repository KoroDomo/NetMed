using NetMed.WebApplicationRefactor.Persistence.Interfaces;
using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Patients;

namespace WebApplicationRefactor.Application.Services
{
    public class PatientsService : IBaseAppService<PatientsApiModel, PatientsApiModel, PatientsApiModel>
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsService> _logger;

        public PatientsService(IPatientsRepository patientsRepository, ILogger<PatientsService> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }
        public async Task<OperationResult> Add(PatientsApiModel dto)
        {
            var result = new OperationResult();
            if (dto == null)
            {
                _logger.LogWarning("Invalid DTO");
                result.success = false;
                result.message = "Invalid DTO";
                return result;
            }
            try
            {
                var patient = await _patientsRepository.Add(dto);
                if (patient == null)
                {
                    _logger.LogError("Failed to add patient");
                    result.success = false;
                    result.message = "Failed to add patient";
                    return result;
                }
                result.success = true;
                result.data = patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding patient");
                result.success = false;
                result.message = "Error adding patient";
            }
            return result;
        }
        public async Task<OperationResult> Delete(PatientsApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                var patient = await _patientsRepository.Delete(dto);
                if (patient == null)
                {
                    _logger.LogError("Failed to delete patient");
                    result.success = false;
                    result.message = "Failed to delete patient";
                    return result;
                }
                result.success = true;
                result.data = patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting patient");
                result.success = false;
                result.message = "Error deleting patient";
            }
            return result;
        }
        public async Task<OperationResult> GetAllData()
        {
            var result = new OperationResult();
            try
            {
                var patients = await _patientsRepository.GetAllData();
                result.success = true;
                result.data = patients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all patients");
                result.success = false;
                result.message = "Error fetching all patients";
            }
            return result;
        }
        public async Task<OperationResult> GetById(int id)
        {
                var result = new OperationResult();
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID");
                result.success = false;
                result.message = "Invalid ID";
                return result;
            }
            try
            {
                var patient = await _patientsRepository.GetById(id);
                if (patient == null)
                {
                    _logger.LogError("Patient not found");
                    result.success = false;
                    result.message = "Patient not found";
                    return result;
                }
                result.success = true;
                result.data = patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching patient by ID");
                result.success = false;
                result.message = "Error fetching patient by ID";
            }
            return result;
        }
        public async Task<OperationResult> Update(PatientsApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                var patient = await _patientsRepository.Update(dto);
                if (patient == null)
                {
                    _logger.LogError("Failed to update patient");
                    result.success = false;
                    result.message = "Failed to update patient";
                    return result;
                }
                result.success = true;
                result.data = patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating patient");
                result.success = false;
                result.message = "Error updating patient";
            }
            return result;
        }


    }
    
}
