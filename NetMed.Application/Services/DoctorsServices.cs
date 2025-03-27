
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Validations.Implementations;
using NetMed.Persistence.BaseLoger.Loger;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
namespace NetMed.Application.Services
{
    public class DoctorsServices : DoctorValidation, IDoctorsServices
    {
        private readonly ILogger<DoctorsServices> _logger;
        private readonly IDoctorsRepository _doctorsRepository;
        public DoctorsServices(IDoctorsRepository doctorsRepository,
            ILogger<DoctorsServices> logger)
          
        {
            if (doctorsRepository is null) throw new ArgumentNullException(nameof(doctorsRepository));
             this._logger = logger;
            this._doctorsRepository = doctorsRepository;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctor = await _doctorsRepository.GetEntityByIdAsync(id);
                if (doctor != null)
                {
                    result.Success = true;
                    result.data = doctor;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Doctor not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " An error occurred while retrieving the doctor.";
            }
            return result;
        }

           
        


        public async Task<OperationResult> GetAllData()
        {
            OperationResult result = new OperationResult();
            try
            {
               

                result.data = await _doctorsRepository.GetAllAsync();

                if(!result.Success)
                {
                    result.data = result.data;
                    result.Success = result.Success;
                    return result ;
                   
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

        public async Task<OperationResult> Add(AddDoctorsDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                _logger.LogInformation("Starting Add method in DoctorsServices.");

                var doctor = new Doctors
                {
                    SpecialtyID = (short)dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseExpirationDate = dto.LicenseExpirationDate,
                    AvailabilityModeId = (short)dto.AvailabilityModeId,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                _logger.LogInformation("Doctor entity created: {@Doctor}", doctor);

                // Perform validations
                var validationResults = new List<OperationResult>
                {
                    ValidateDoctorAvailability(doctor),
                    ValidateDoctorClinicAddress(doctor),
                    ValidateDoctorConsultationFee(doctor),
                    ValidateDoctorLicenseNumber(doctor),
                    ValidateDoctorSpecialtyID(doctor),
                    ValidateDoctorYearsOfExperience(doctor),
                    ValidateDoctorLicenseExpirationDate(doctor)
                };

                // Check validation 
                var failedValidation = validationResults.FirstOrDefault(v => !v.Success);
                if (failedValidation != null)
                {
                    _logger.LogWarning("Validation failed: {Message}", failedValidation.Message);
                    return failedValidation;
                }

                _logger.LogInformation("All validations passed.");

                OperationResult saveResult = await _doctorsRepository.SaveEntityAsync(doctor);
                if (saveResult == null)
                {
                    result.Success = false;
                    result.Message = "Failed to save doctor entity.";
                    _logger.LogError("SaveEntityAsync returned null.");
                }
                else
                {
                    result = saveResult;
                    _logger.LogInformation("Doctor entity saved: {@Result}", result);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                if (ex.InnerException != null)
                {
                    result.Message += " Inner exception: " + ex.InnerException.Message;
                }
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateDoctorsDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = new Doctors
                {
                    UserId = dto.UserId,
                    SpecialtyID = dto.SpecialtyID,
                    LicenseNumber = dto.LicenseNumber,
                    PhoneNumber = dto.PhoneNumber,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseExpirationDate = dto.LicenseExpirationDate,
                    AvailabilityModeId = (short)dto.AvailabilityModeId
                };
                var doc = await _doctorsRepository.UpdateEntityAsync(doctor);
                result.data = doc;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }

        public async Task<OperationResult> Delete(DeleteDoctorDto dto)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = new Doctors
                {
                    LicenseExpirationDate = dto.LicenseExpirationDate,
                    ClinicAddress = dto.ClinicAddress,
                    PhoneNumber = dto.PhoneNumber,
                    LicenseNumber = dto.LicenseNumber,
                    UserId = dto.UserId,
                    Education = dto.Education,
                    SpecialtyID = dto.SpecialtyID,
                    AvailabilityModeId = (short)dto.AvailabilityModeId

                };
                var doc = await _doctorsRepository.DeleteEntityAsync(doctor);
                result.data = doc;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error eliminando los datos.";
                _logger.LogError(ex, ex.Message);
            }
            return result;
        }
    }
}
