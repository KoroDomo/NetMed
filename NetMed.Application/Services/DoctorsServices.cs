
using Microsoft.Extensions.Logging;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Validations.Implementations;
using NetMed.Persistence.Interfaces;
namespace NetMed.Application.Services
{
    public class DoctorsServices : DoctorValidation, IDoctorsServices
    {
        private readonly ILogger<DoctorsServices> logger;
        private readonly IDoctorsRepository _doctorsRepository;
        public DoctorsServices(IDoctorsRepository doctorsRepository,
            ILogger<DoctorsServices> logger)
          
        {
            if (doctorsRepository is null) throw new ArgumentNullException(nameof(doctorsRepository));
            this.logger = logger;
            this._doctorsRepository = doctorsRepository;
        }

        public async Task<OperationResult> GetById(int id)
        {
            OperationResult response= new OperationResult();
            try
            {
                var result =  await _doctorsRepository.GetEntityByIdAsync(id);

                if(!result.Success)
                {
                    response.data = result.data;
                    response.Success = result.Success;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
                this.logger.LogError(ex.Message);
            }

            return response;
        }


        public async Task<OperationResult> GetAllData()
        {
            OperationResult response = new OperationResult();
            try
            {
               var  result = await _doctorsRepository.GetAllAsync();

                if(!result.Success)
                {
                    response.data = result.data;
                    response.Success = result.Success;
                    return response ;
                   
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return response;
        }

        public async Task<OperationResult> Add(AddDoctorsDto dto)
        {
            OperationResult result = new OperationResult();
            try
            {
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
                    return failedValidation;
                }

                result = await _doctorsRepository.SaveEntityAsync(doctor);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
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
            }
            return result;
        }
    }
}
