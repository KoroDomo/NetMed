
using NetMed.Application.Dtos.Patients;



namespace NetMed.Application.Dtos
{
    public static class PatientsConvertToDto
    {
        public static PatientsDto ConvertToDto(this Domain.Entities.Patients entity)
        {
            return new PatientsDto
            {
                Id = entity.Id,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
                EmergencyContactName = entity.EmergencyContactName,
                EmergencyContactPhone = entity.EmergencyContactPhone,
                BloodType = entity.BloodType,
                Allergies = entity.Allergies,
                InsuranceProviderID = entity.InsuranceProviderID,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
            };
        }
    }
}