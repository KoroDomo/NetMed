

using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validations.Implementations;
using NetMed.Infrastructure.Validations.Interfaces;

namespace NetMed.Infrastructure.Validations.Implementations
{
    public class DoctorValidation : OperationValidator, IDoctorValidations
    {
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
            if(doctors.LicenseNumber == null)
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
           if(doctors.SpecialtyID < 1 && doctors.SpecialtyID < 0)
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
            if (doctors.LicenseExpirationDate.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
            {
                return new OperationResult
                {
                    Message = "Valid License Expiration Date",
                    Success = true
                };
            }
            return new OperationResult
            {
                Message = "Invalid License Expiration Date",
                Success = false
            };
        }
    }
}
