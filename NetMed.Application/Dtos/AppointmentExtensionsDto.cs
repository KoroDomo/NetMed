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
                AppointmentID = entity.Id,
                PatientID = entity.PatientID,
                DoctorID = entity.DoctorID,
                AppointmentDate = entity.AppointmentDate,
                StatusID = entity.StatusID,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static List<AppointmentsDto> ToDtoList(this IEnumerable<NetMed.Domain.Entities.Appointments> entities)
        {
            return entities.Select(e => new AppointmentsDto
            {
                AppointmentID = e.Id,
                PatientID = e.PatientID,
                DoctorID = e.DoctorID,
                AppointmentDate = e.AppointmentDate,
                StatusID = e.StatusID,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToList();
        }
    }

}
