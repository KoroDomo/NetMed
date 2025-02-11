
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories.Interfaces;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
namespace NetMed.Persistence.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctors, int>, IDoctorsRepository
    {
        private readonly NetMedContext _context;
        public DoctorsRepository(NetMedContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctors?> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<OperationResult> GetByAvailabilityModeAsync(int availabilityModeId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.AvailabilityModeId == availabilityModeId).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetBySpecialtyAsync(int specialtyId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.SpecialtyId == specialtyId).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetByLicenseNumberAsync(string licenseNumber)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.LicenseNumber == licenseNumber).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetActiveDoctorsAsync(bool isActive)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.IsActive == isActive).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsByExperienceAsync(int minYears, int maxYears)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.YearsOfExperience >= minYears && x.YearsOfExperience <= maxYears).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsByConsultationFeeAsync(decimal minFee, decimal maxFee)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.ConsultationFee >= minFee && x.ConsultationFee <= maxFee).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await _context.Doctors.Where(x => x.LicenseExpirationDate == expirationDate).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

    }
}
