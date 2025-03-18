using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validation.Interfaces;

namespace NetMed.Infrastructure.Validations.Interfaces
{

    public interface IDoctorValidations : IOperationValidator
    {
        public OperationResult ValidateDoctorAvailability(Doctors doctors);
        public OperationResult ValidateDoctorLicenseNumber(Doctors doctors);
        public OperationResult ValidateDoctorSpecialtyID(Doctors doctors);
        public OperationResult ValidateDoctorYearsOfExperience(Doctors doctors);

        public OperationResult ValidateDoctorConsultationFee(Doctors doctors);

        public OperationResult ValidateDoctorClinicAddress(Doctors doctors);

        public OperationResult ValidateDoctorLicenseExpirationDate(Doctors doctors);
    }
}
