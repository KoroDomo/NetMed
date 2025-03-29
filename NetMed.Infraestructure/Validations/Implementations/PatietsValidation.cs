
using Microsoft.IdentityModel.Tokens;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Validations.Implementations;
using NetMed.Infrastructure.Validations.Interfaces;


namespace NetMed.Infrastructure.Validations.Implementations
{
    public class PatietsValidation : OperationValidator, IPatientsValidations
    {
        public OperationResult ValidatePatientAddress(Patients patients)
        {
            OperationResult result = new OperationResult();
            if (string.IsNullOrEmpty(patients.Address) || (patients.Address.Length < 3))
            {
                result.Success = false;
                result.Message = "Invalid Address";
                return result;

            }
            return result;
        }
        public OperationResult ValidatePatientAge(Patients patients)
        {
            OperationResult result = new OperationResult();
            if (patients.DateOfBirth.Year < 1900 ||   patients.DateOfBirth.Year > 2025)
            {

                result.Success = false;
                result.Message = "Invalid Date of Birth";
                return result;
            }
            return result;
        }

        public OperationResult ValidatePatientBloodType(Patients patients)
        {
            OperationResult result = new OperationResult();
            if (patients.BloodType == '\0' || !IsValidBloodType(patients.BloodType))
            {
                result.Success = false;
                result.Message = "Invalid Blood Type";
                return result;
            }
            return result;
        }

        private bool IsValidBloodType(char bloodType)
        {
            char[] validBloodTypes = { 'A', 'B', 'O' };
            return validBloodTypes.Contains(bloodType);
        }

    

        public OperationResult ValidatePatientEmergencyContact(Patients patients)
        {
            OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(patients.EmergencyContactName) || (patients.EmergencyContactName.Length < 4))
            {
                result.Success = false;
                result.Message = "Invalid Emergency Contact Name";
                return result;
            }
            return result;
        }

        public OperationResult ValidatePatientGender(Patients patients)
        {
            OperationResult result = new OperationResult();

            if (patients.Gender == '\0' || (patients.Gender != 'M' && patients.Gender != 'F'))
            {
                result.Success = false;
                result.Message = "Invalid Gender";
                return result;
            }
            return result;
        }

        public OperationResult ValidatePatientInsuranceProvider(Patients patients)
        {
            OperationResult result = new OperationResult();

            if(patients.InsuranceProviderID== 0)
            {
                result.Success = false;
                result.Message = "Invalid Insurance Provider";
                return result;
            }
          return result;
        }

   

  public OperationResult ValidatePatientPhoneNumber(Patients patients)
        {
            OperationResult result = new OperationResult();

            if (string.IsNullOrEmpty(patients.PhoneNumber) || (patients.PhoneNumber.Length != 10))
            {
                result.Success = false;
                result.Message = "Invalid Phone Number";
                return result;
            }

            result.Success = true;
            result.data = patients;
            return result; 
        }

        public OperationResult ValidatePatientWithoutInsurance(Patients patients)
        {
            OperationResult result = new OperationResult();

            if (patients.InsuranceProviderID == 0)
            {
                result.Success = false;
                result.Message = "Patient has no insurance";
            }

                return result;
            }

        public OperationResult ValidatePatientAllergies(Patients patients)
        {
            OperationResult result = new OperationResult();
            if (string.IsNullOrEmpty(patients.Allergies) || (patients.Allergies.Length < 3))
            {
                result.Success = false;
                result.Message = "Invalid Allergies";
                return result;
            }
            return result;
        }
    }
}
