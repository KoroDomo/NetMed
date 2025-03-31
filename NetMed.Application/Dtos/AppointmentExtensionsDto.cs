using NetMed.Application.Dtos.Appointments;
using NetMed.Domain.Entities;
using NetMed.Model.Models;


namespace NetMed.Application.Dtos
{
    public static class AppointmentExtensions
    {
        public static AppointmentsDto ToDto(this NetMed.Domain.Entities.Appointments entity)
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
        public static List<AppointmentsDto> ToDtoList(this IEnumerable<NetMed.Domain.Entities.Appointments> entities)
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
    }

}
