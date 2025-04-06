using NetMed.WebApplicationRefactor.Persistence.Interfaces;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Doctors;
using System.Text.RegularExpressions;

namespace WebApplicationRefactor.Application.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorServices> _logger;

        public DoctorServices(IDoctorRepository doctorRepository, ILogger<DoctorServices> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
        }

        public async Task<OperationResult> GetAllData()
        {
            var result = new OperationResult();
            try
            {
                var doctors = await _doctorRepository.GetAllData();
                result.Success = true;
                result.data = doctors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all doctors");
                result.Success = false;
                result.message = "Error fetching all doctors";
            }
            return result;
        }

        public async Task<OperationResult> GetById(int id)
        {
            var result = new OperationResult();
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID");
                result.Success = false;
                result.message = "Invalid ID";
                return result;
            }
            try
            {
                var doctor = await _doctorRepository.GetById(id);
                if (doctor == null)
                {
                    _logger.LogError("Doctor not found");
                    result.Success = false;
                    result.message = "Doctor not found";
                    return result;
                }
                result.Success = true;
                result.data = doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching doctor by ID");
                result.Success = false;
                result.message = "Error fetching doctor by ID";
            }
            return result;
        }

        public async Task<OperationResult> Add(DoctorsApiModel dto)
        {
            var validationResult = ValidateDoctor(dto);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            var result = new OperationResult();
            try
            {
                await _doctorRepository.Add(dto);
                result.message = "Doctor added Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding doctor");
                result.Success = false;
                result.message = "Error adding doctor";
            }
            return result;
        }

        public async Task<OperationResult> Update(DoctorsApiModel dto)
        {
            var validationResult = ValidateDoctor(dto);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            var result = new OperationResult();
            try
            {
                await _doctorRepository.Update(dto);
                result.Success = true;
                result.message = "Doctor updated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor");
                result.Success = false;
                result.message = "Error updating doctor";
            }
            return result;
        }

        public async Task<OperationResult> Delete(DoctorsApiModel dto)
        {
            var result = new OperationResult();
            try
            {
                await _doctorRepository.Delete(dto);
                result.Success = true;
                result.message = "Doctor deleted Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor");
                result.Success = false;
                result.message = "Error deleting doctor";
            }
            return result;
        }

        private OperationResult ValidateDoctor(DoctorsApiModel doctor)
        {
            if (doctor == null)
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor is null"
                };
            }

            if (string.IsNullOrWhiteSpace(doctor.LicenseNumber))
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor name is empty"
                };
            }

            if (string.IsNullOrWhiteSpace(doctor.ClinicAddress) || doctor.ClinicAddress.Length > 55)
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor address is invalid"
                };
            }

            if (doctor.Bio.Length < 20)
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor  is too short"
                };
            }

            if (!IsValidDoctorBio(doctor.Bio))
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor bio contains invalid characters"
                };
            }

            if (doctor.ClinicAddress.Length < 5)
            {
                return new OperationResult
                {
                    Success = false,
                    message = "Doctor Address is too short"
                };
            }

            return new OperationResult { Success = true };
        }

        private bool IsValidDoctorBio(string bio)
        {
            var regex = new Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-_(),.]+$");
            return regex.IsMatch(bio);
        }
    }
}