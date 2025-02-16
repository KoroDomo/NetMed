
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NetMed.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
namespace NetMed.Persistence.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<DoctorsRepository> _logger;
        public DoctorsRepository(NetMedContext context,
            ILogger<DoctorsRepository> logger) : base(context)
        {
                _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> GetByAvailabilityModeAsync(int availabilityModeId)
        {
            OperationResult result = new OperationResult();
            try
            {
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.AvailabilityModeId == availabilityModeId).ToListAsync();
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
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.SpecialtyId == specialtyId).ToListAsync();
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
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.LicenseNumber == licenseNumber).ToListAsync();
    
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public async Task<OperationResult> GetActiveDoctorsAsync(bool isActive = true)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.data = await _context.Doctors.Where(x => x.IsActive == isActive).ToListAsync();
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
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.YearsOfExperience >= minYears && x.YearsOfExperience <= maxYears).ToListAsync();
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
            List<Doctors> data = new List<Doctors>();
            try
            {
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.ConsultationFee >= minFee && x.ConsultationFee <= maxFee).ToListAsync();
         
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
            List<Doctors> data = new List<Doctors>();
            try
            {
                if(result.data == null)
                {
                    result.Message = "No se encontraron datos";
                    result.Success = false;
                }
                result.data = await _context.Doctors.Where(x => x.LicenseExpirationDate == expirationDate).ToListAsync();
                
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(Doctors entity)
        {
            var result = new OperationResult();
            try
            {
                _context.Doctors.Add(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error guardando los datos.";
                result.Success = false;
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(Doctors entity)
        {
            var result = new OperationResult();
            try
            {
                _context.Doctors.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + " Ocurrio un error actualizando los datos.";
                result.Success = false;
            }
            return result;
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Doctors, bool>> filter)
        {
            var result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return result;
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Doctors, bool>> filter)
        {
            return await _context.Doctors.AnyAsync(filter);

        }

        public override async Task<Doctors> GetEntityByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id) ?? throw new InvalidOperationException("Entity not found");
        }


    }
}
