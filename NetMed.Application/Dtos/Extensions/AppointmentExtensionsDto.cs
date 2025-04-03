
using DomainAppointments = NetMed.Domain.Entities.Appointments;
using NetMed.Application.Dtos.Appointments;

namespace NetMed.Application.Dtos.Extensions
{
    public static class AppointmentExtensionsDto
    {
        public static AppointmentsDto ToDto(this DomainAppointments entity)
        {
            return new AppointmentsDto
            {
                appointmentID = entity.Id,
                patientID = entity.PatientID,
                doctorID = entity.DoctorID,
                appointmentDate = entity.AppointmentDate,
                statusID = entity.StatusID,
                createdAt = entity.CreatedAt,
                updatedAt = entity.UpdatedAt
            };
        }
        public static List<AppointmentsDto> ToDtoList(this IEnumerable<DomainAppointments> entities)
        {
            return entities.Select(e => new AppointmentsDto
            {
                appointmentID = e.Id,
                patientID = e.PatientID,
                doctorID = e.DoctorID,
                appointmentDate = e.AppointmentDate,
                statusID = e.StatusID,
                createdAt = e.CreatedAt,
                updatedAt = e.UpdatedAt
            }).ToList();
        }
        public static DomainAppointments ConvertToEntitySave(this SaveAppointmentsDto dto)
        {
            return new DomainAppointments
            {
                PatientID = dto.patientID,
                DoctorID = dto.doctorID,
                AppointmentDate = dto.appointmentDate,
                StatusID = dto.statusID
            };
        }
        public static DomainAppointments ConvertToEntityUpdate(this UpdateAppointmentsDto dto)
        {
            return new DomainAppointments
            {
                Id = dto.appointmentID,
                PatientID = dto.patientID,
                DoctorID = dto.doctorID,
                AppointmentDate = dto.appointmentDate,
                StatusID = dto.statusID
            };
        }

    }
}
    
