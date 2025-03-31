
using NetMed.Application.Dtos.DoctorAvailability;

namespace NetMed.Application.Dtos
{
    public static class DoctorAvailabilityExtensionsDto
    {
        public static DoctorAvailabilityDto ToDto(this NetMed.Domain.Entities.DoctorAvailability entity)
        {
            return new DoctorAvailabilityDto
            {
                availabilityID = entity.Id,
                doctorID = entity.DoctorID,
                availableDate = entity.AvailableDate,
                startTime = entity.StartTime,
                endTime = entity.EndTime,
            };
        }
        public static List<DoctorAvailabilityDto> ToDtoList(this IEnumerable<NetMed.Domain.Entities.DoctorAvailability> entities)
        {
            return entities.Select(e => new DoctorAvailabilityDto
            {
                availabilityID = e.Id,
                doctorID = e.DoctorID,
                availableDate = e.AvailableDate,
                startTime = e.StartTime,
                endTime = e.EndTime
            }).ToList();
        }
    }
}
