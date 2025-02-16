
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IDoctorAvailabilityRepository : IBaseRepository<DoctorAvailability>
    {
        Task<OperationResult> GetDoctorAvailabilityByAppointments(int AppointmentID);
        Task<OperationResult> SetAvailabilityAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime);
        Task<DoctorAvailability> GetAvailabilityByDoctorAndDateAsync(int DoctorID, DateOnly AvailableDate);
        Task<OperationResult> UpdateAvailabilityAsync(int availabilityId, int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime);
        Task<OperationResult> RemoveAvailabilityAsync(int AvailabilityID);
        Task<bool> IsDoctorAvailableAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime);
        Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int DoctorID, DateOnly AvailableDate, TimeOnly StartTime, TimeOnly EndTime, bool isAvailable);

    }
}
