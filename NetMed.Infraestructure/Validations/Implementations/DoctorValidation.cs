

using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validations.Implementations;
using NetMed.Infrastructure.Validations.Interfaces;

namespace NetMed.Infrastructure.Validations.Implementations
{
    public class DoctorValidation : OperationValidator, IDoctorValidations
    {
        private readonly ILogger<DoctorValidation> _logger;

        public DoctorValidation(ILogger<DoctorValidation> logger)
        {
            _logger = logger;
        }

        public OperationResult ValidateDoctorAvailability(Doctors doctors)
        {
            if(doctors.AvailabilityModeId < 1 && doctors.AvailabilityModeId < 0 )
            {
                return new OperationResult
                {
                    Message = "Invalid Availability Mode",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid Availability Mode",
                Success = true
            };
        }

        public OperationResult ValidateDoctorClinicAddress(Doctors doctors)
        {
            if(string.IsNullOrWhiteSpace(doctors.ClinicAddress) && doctors.ClinicAddress.Length < 4)
            {
                return new OperationResult
                {
                    Message = "Invalid Clinic Address",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid Clinic Address",
                Success = true
            };

        }

        public OperationResult ValidateDoctorConsultationFee(Doctors doctors)
        {
           if(doctors.ConsultationFee < 0)
            {
                return new OperationResult
                {
                    Message = "Invalid Consultation Fee",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid Consultation Fee",
                Success = true
            };

        }

        public OperationResult ValidateDoctorLicenseNumber(Doctors doctors)
        {
            if (string.IsNullOrWhiteSpace(doctors.LicenseNumber))
            {
                return new OperationResult
                {
                    Message = "Invalid License Number",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid License Number",
                Success = true
            };
        }

        public OperationResult ValidateDoctorSpecialtyID(Doctors doctors)
        {
           if(doctors.SpecialtyID > 8 || doctors.SpecialtyID < 0)
            {
                return new OperationResult
                {
                    Message = "Invalid Specialty ID",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid Specialty ID",
                Success = true
            };
        }

        public OperationResult ValidateDoctorYearsOfExperience(Doctors doctors)
        {
            if (doctors.YearsOfExperience < 0)
            {
                return new OperationResult
                {
                    Message = "Invalid Years of Experience",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "Valid Years of Experience",
                Success = true
            };
        }

        public OperationResult ValidateDoctorLicenseExpirationDate(Doctors doctors)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            _logger.LogInformation($"Validating License Expiration Date: {doctors.LicenseExpirationDate}, Current Date: {currentDate}");

            if (doctors.LicenseExpirationDate < currentDate)
            {
                return new OperationResult
                {
                    Message = "License is expired",
                    Success = false
                };
            }
            return new OperationResult
            {
                Message = "License is valid",
                Success = true
            };
        }

    }
}
