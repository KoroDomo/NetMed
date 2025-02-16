using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IAppointmentsRespository : IBaseRepository<Appointments>
    {
        Task<OperationResult> CreateAppointmentAsync(int PatientID, int DoctorID, DateTime AppointmentDate);
        Task<OperationResult> GetAppointmentByIdAsync(int AppointmentID);
        Task<OperationResult> GetAppointmentsByPatientAsync(int PatientID);
        Task<OperationResult> GetAppointmentsByDoctorAsync(int DoctorID);
        Task<OperationResult> UpdateAppointmentStatusAsync(int AppointmentID, int StatusID);
        Task<OperationResult> GetAppointmentsByStatusAsync(int StatusID);
        Task<OperationResult> GetAppointmentsByDateAsync(DateTime AppointmentDate);
        Task<OperationResult> CancelAppointmentAsync(int AppointmentID);
        Task<OperationResult> GetAppointmentsByDoctorAndDateAsync(int DoctorID, DateTime AppointmentDate);
    }
}
