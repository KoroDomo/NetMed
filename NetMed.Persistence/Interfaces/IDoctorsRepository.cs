using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;



namespace NetMed.Persistence.Interfaces
{
    public interface IDoctorsRepository : IBaseRepository<Doctors>
    {
        Task<List<Doctors>> GetByAvailabilityModeAsync(int availabilityModeId);

        Task<List<Doctors>> GetBySpecialtyAsync(int specialtyId);

        Task<List<Doctors>> GetByLicenseNumberAsync(string licenseNumber);

        Task<List<Doctors>> GetActiveDoctorsAsync(bool isActive = true);

        Task<List<Doctors>> GetDoctorsByExperienceAsync(int minYears, int maxYears);

        Task<List<Doctors>> GetDoctorsByConsultationFeeAsync(decimal minFee, decimal maxFee);

        Task<List<Doctors>> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate);

    }
}
