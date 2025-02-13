
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NetMed.Persistence.Interfaces;
namespace NetMed.Persistence.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly NetMedContext _context;
        public DoctorsRepository(NetMedContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Doctors>> GetByAvailabilityModeAsync(int availabilityModeId)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.AvailabilityModeId == availabilityModeId).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetBySpecialtyAsync(int specialtyId)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.SpecialtyId == specialtyId).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetByLicenseNumberAsync(string licenseNumber)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.LicenseNumber == licenseNumber).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetActiveDoctorsAsync(bool isActive = true)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.IsActive == isActive).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetDoctorsByExperienceAsync(int minYears, int maxYears)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();

            try
            {
                data = await _context.Doctors.Where(x => x.YearsOfExperience >= minYears && x.YearsOfExperience <= maxYears).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetDoctorsByConsultationFeeAsync(decimal minFee, decimal maxFee)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.ConsultationFee >= minFee && x.ConsultationFee <= maxFee).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
        }

        public async Task<List<Doctors>> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate)
        {
            var result = new OperationResult();
            List<Doctors> data = new List<Doctors>();
            try
            {
                data = await _context.Doctors.Where(x => x.LicenseExpirationDate == expirationDate).ToListAsync();
                result.Result = data;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + " Ocurrio un error obteniendo los datos.";
            }
            return data;
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
                result.Result = await _context.Doctors.Where(filter).ToListAsync();
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
