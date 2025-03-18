using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validation.Interfaces;

namespace NetMed.Infrastructure.Validations.Interfaces
{
  public interface IPatientsValidations : IOperationValidator

    {
        public OperationResult ValidatePatientAge(Patients patients);
        public OperationResult ValidatePatientAddress(Patients patients);

        public OperationResult ValidatePatientPhoneNumber(Patients patients);
    
        public OperationResult ValidatePatientGender(Patients patients);

        public OperationResult ValidatePatientBloodType(Patients patients);

        public OperationResult ValidatePatientEmergencyContact(Patients patients);

        public OperationResult ValidatePatientWithoutInsurance(Patients patients);

        public OperationResult ValidatePatientInsuranceProvider(Patients patients);

    }
}
