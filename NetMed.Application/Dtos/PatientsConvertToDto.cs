//using NetMed.Application.Dtos.PatientsDto;

//using NetMed.Domain.Entities;

//namespace NetMed.Application.Dtos
//{
//    public static class PatientsConvertToDto
//    {
//        public static PatientsDto ConvertToDto(this NetMed.Domain.Entities.Patients entity)
//        {
//            return new PatientsDto
//            {
//                Id = entity.Id,
//                DateOfBirth = entity.DateOfBirth,
//                Gender = entity.Gender,
//                EmergencyContactName = entity.EmergencyContactName,
//                EmergencyContactPhone = entity.EmergencyContactPhone,
//                BloodType = entity.BloodType,
//                Allergies = entity.Allergies,
//                InsuranceProviderID = entity.InsuranceProviderID,
//                PhoneNumber = entity.PhoneNumber,
//                Address = entity.Address,
//                createdAt = entity.createdAt,
//                UpdatedAt = entity.UpdatedAt,
//                isActive = entity.isActive
//            };
//        }
//    }
//}