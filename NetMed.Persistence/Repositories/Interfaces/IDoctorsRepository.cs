﻿using System.Threading.Tasks;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;



namespace NetMed.Persistence.Repositories.Interfaces
{
    public interface  IDoctorsRepository : IBaseRepository<Doctors, int>
    {
        Task<OperationResult> GetByAvailabilityModeAsync(int availabilityModeId);

        Task<OperationResult> GetBySpecialtyAsync(int specialtyId);

        Task<OperationResult> GetByLicenseNumberAsync(string licenseNumber);

        Task<OperationResult> GetActiveDoctorsAsync(bool isActive = true);

        Task<OperationResult> GetDoctorsByExperienceAsync(int minYears, int maxYears);

        Task<OperationResult> GetDoctorsByConsultationFeeAsync(decimal minFee, decimal maxFee);

        Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate);

    }
}
