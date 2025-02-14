using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IAppointmentsRespository : IBaseRepository<Appointments>
    {
        Task<OperationResult> CreateAppointmentAsync(int patientId, int doctorId, DateOnly appointmentDate);
        Task<Appointments> GetAppointmentByIdAsync(int appointmentId);
        Task<List<Appointments>> GetAppointmentsByPatientAsync(int patientId);
        Task<List<Appointments>> GetAppointmentsByDoctorAsync(int doctorId);
        Task<OperationResult> UpdateAppointmentStatusAsync(int appointmentId, int statusId);
        Task<List<Appointments>> GetAppointmentsByStatusAsync(int statusId);
        Task<List<Appointments>> GetAppointmentsByDateAsync(DateOnly appointmentDate);
        Task<OperationResult> CancelAppointmentAsync(int appointmentId);
        Task<List<Appointments>> GetAppointmentsByPatientAndDateAsync(int patientId, DateOnly appointmentDate);
        Task<List<Appointments>> GetAppointmentsByDoctorAndDateAsync(int doctorId, DateOnly appointmentDate);
        Task<List<Appointments>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
