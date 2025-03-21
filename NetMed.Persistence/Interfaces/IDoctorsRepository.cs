using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;



namespace NetMed.Persistence.Interfaces
{
    public interface IDoctorsRepository : IBaseRepository<Doctors>
    {
        Task<OperationResult> GetByAvailabilityModeAsync(short availabilityModeId);

        Task<OperationResult> GetBySpecialtyAsync(int specialtyId);

        Task<OperationResult> GetByLicenseNumberAsync(string licenseNumber);

        Task<OperationResult> GetActiveDoctorsAsync(bool isActive);

        Task<OperationResult> GetDoctorsByExperienceAsync(int expYears);

    
        Task<OperationResult> GetDoctorsByConsultationFeeAsync(decimal avrFee);

        Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate);
    }
}
