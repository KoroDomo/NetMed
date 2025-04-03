
using DomainDoctorAvailability = NetMed.Domain.Entities.DoctorAvailability;
using NetMed.Application.Dtos.DoctorAvailability;


namespace NetMed.Application.Dtos.Extensions
{
    public static class DoctorAvailabilityExtensionsDto
    {
        public static DoctorAvailabilityDto ToDto(this DomainDoctorAvailability entity)
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
        public static List<DoctorAvailabilityDto> ToDtoList(this IEnumerable<DomainDoctorAvailability> entities)
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
        public static DomainDoctorAvailability ConvertToEntitySave(this SaveDoctorAvailabilityDto save)
        {
            return new DomainDoctorAvailability
            {
                DoctorID = save.doctorID,
                AvailableDate = save.availableDate,
                StartTime = save.startTime,
                EndTime = save.endTime
            };
        }
        public static DomainDoctorAvailability ConvertToEntityUpdate(this UpdateDoctorAvailabilityDto update)
        {
            return new DomainDoctorAvailability
            {
                Id = update.availabilityID,
                DoctorID = update.doctorID,
                AvailableDate = update.availableDate,
                StartTime = update.startTime,
                EndTime = update.endTime
            };
        }
    }
}
