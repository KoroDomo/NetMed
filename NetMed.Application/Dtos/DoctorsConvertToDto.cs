

using NetMed.Application.Dtos.Doctors;

namespace NetMed.Application.Dtos
{
    public static class DoctorsConvertToDto
    {
        public static DoctorsDto ConvertToDto(this Domain.Entities.Doctors entity)
        {
            return new DoctorsDto
            {
                Id = entity.Id,
                SpecialtyID = entity.SpecialtyID,
                LicenseNumber = entity.LicenseNumber,
                PhoneNumber = entity.PhoneNumber,
                YearsOfExperience = entity.YearsOfExperience,
                Education = entity.Education,
                Bio = entity.Bio,
                ConsultationFee = entity.ConsultationFee,
                ClinicAddress = entity.ClinicAddress,
                AvailabilityModeId = entity.AvailabilityModeId,
                LicenseExpirationDate = entity.LicenseExpirationDate
            };
        }

        public static List<DoctorsDto> ConvertToDoctorList(this IEnumerable<NetMed.Domain.Entities.Doctors> entities)
        {
            return entities.Select(e => new DoctorsDto
            {
                Id = e.Id,
                SpecialtyID = e.SpecialtyID,
                LicenseNumber = e.LicenseNumber,
                PhoneNumber = e.PhoneNumber,
                YearsOfExperience = e.YearsOfExperience,
                Education = e.Education,
                Bio = e.Bio,
                ConsultationFee = e.ConsultationFee,
                ClinicAddress = e.ClinicAddress,
                AvailabilityModeId = e.AvailabilityModeId,
                LicenseExpirationDate = e.LicenseExpirationDate
            }).ToList();
        }
    }
}
