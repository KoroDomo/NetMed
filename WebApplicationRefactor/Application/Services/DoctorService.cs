using NetMed.WebApplicationRefactor.Persistence.Interfaces;

using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Doctors;
using System.Text.RegularExpressions;
using WebApplicationRefactor.Application.Contracts;

namespace WebApplicationRefactor.Application.Services
{
    public class DoctorService : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IDoctorRepository doctorRepository, ILogger<DoctorService> logger)
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
                result.success = true;
                result.data = doctors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all doctors");
                result.success = false;
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
                result.success = false;
                result.message = "Invalid ID";
                return result;
            }
            try
            {
                var doctor = await _doctorRepository.GetById(id);
                if (doctor == null)
                {
                    _logger.LogError("Doctor not found");
                    result.success = false;
                    result.message = "Doctor not found";
                    return result;
                }
                result.success = true;
                result.data = doctor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching doctor by ID");
                result.success = false;
                result.message = "Error fetching doctor by ID";
            }
            return result;
        }

        public async Task<OperationResult> Add(DoctorsApiModel dto)
        {
            var validationResult = ValidateDoctor(dto);
            if (!validationResult.success)
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
                result.success = false;
                result.message = "Error adding doctor";
            }
            return result;
        }

        public async Task<OperationResult> Update(DoctorsApiModel dto)
        {
            var validationResult = ValidateDoctor(dto);
            if (!validationResult.success)
            {
                return validationResult;
            }

            var result = new OperationResult();
            try
            {
                await _doctorRepository.Update(dto);
                result.success = true;
                result.message = "Doctor updated Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating doctor");
                result.success = false;
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
                result.success = true;
                result.message = "Doctor deleted Successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting doctor");
                result.success = false;
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
                    success = false,
                    message = "Doctor is null"
                };
            }

            if (string.IsNullOrWhiteSpace(doctor.LicenseNumber))
            {
                return new OperationResult
                {
                    success = false,
                    message = "Doctor name is empty"
                };
            }

            if (string.IsNullOrWhiteSpace(doctor.ClinicAddress) || doctor.ClinicAddress.Length > 55)
            {
                return new OperationResult
                {
                    success = false,
                    message = "Doctor address is invalid"
                };
            }

            if (doctor.Bio.Length < 20)
            {
                return new OperationResult
                {
                    success = false,
                    message = "Doctor  is too short"
                };
            }

            if (!IsValidDoctorBio(doctor.Bio))
            {
                return new OperationResult
                {
                    success = false,
                    message = "Doctor bio contains invalid characters"
                };
            }

            if (doctor.ClinicAddress.Length < 5)
            {
                return new OperationResult
                {
                    success = false,
                    message = "Doctor Address is too short"
                };
            }

            return new OperationResult { success = true };
        }

        private bool IsValidDoctorBio(string bio)
        {
            var regex = new Regex(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-_(),.]+$");
            return regex.IsMatch(bio);
        }
    }
}