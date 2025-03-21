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

        Task<OperationResult> GetDoctorsByExperienceAsync(int minYears, int maxYears);
>>>>>>> 7429c8c09c80462f0e67b22146091a9a5c5357e4

        Task<OperationResult> GetActiveDoctorsAsync(bool isActive);

        Task<OperationResult> GetDoctorsByExperienceAsync(int Years);

        Task<OperationResult> GetDoctorsByConsultationFeeAsync(decimal avrFee);

        Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate);
    }
}
