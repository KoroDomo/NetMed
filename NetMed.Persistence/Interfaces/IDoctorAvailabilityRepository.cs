
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;

namespace NetMed.Persistence.Interfaces
{
    public interface IDoctorAvailabilityRepository : IBaseRepository<DoctorAvailability>
    {
        Task <OperationResult> GetDoctorAvailabilityByAppointments(int AppointmentID);
        Task<OperationResult> SetAvailabilityAsync(int doctorId, DateOnly availableDate, TimeOnly startTime, TimeOnly endTime);
        Task<List<DoctorAvailability>> GetAvailabilityByDoctorAndDateAsync(int doctorId, DateOnly AvailableDate);
        Task<List<DoctorAvailability>> GetGeneralAvailabilityByDoctorAsync(int doctorId);
        Task<OperationResult> UpdateAvailabilityAsync(int availabilityId, int doctorId, DateOnly availableDate, TimeOnly startTime, TimeOnly endTime);
        Task<OperationResult> RemoveAvailabilityAsync(int availabilityId);
        Task<bool> IsDoctorAvailableAsync(int doctorId, DateOnly availableDate, TimeOnly startTime, TimeOnly endTime);
        Task<List<DoctorAvailability>> GetFutureAvailabilityByDoctorAsync(int doctorId);
        Task<OperationResult> UpdateAvailabilityInRealTimeAsync(int doctorId, DateOnly availableDate, TimeOnly startTime, TimeOnly endTime, bool isAvailable);

    }
}
