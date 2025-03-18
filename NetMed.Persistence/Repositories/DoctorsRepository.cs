
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NetMed.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
namespace NetMed.Persistence.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly IRepErrorMapper _repErrorMapper;
        private readonly NetMedContext _context;
        private readonly ILogger<DoctorsRepository> _logger;
        public DoctorsRepository(NetMedContext context,
            ILogger<DoctorsRepository> logger,
            IRepErrorMapper repErrorMapper) : base(context)
        {
            _context = context;
            _logger = logger;
            _repErrorMapper = repErrorMapper;
        }

        public async Task<OperationResult> GetByAvailabilityModeAsync(short availabilityModeId)
        {
            OperationResult result = new OperationResult();
            try
            {

                result.data = await _context.Doctors.Where(x => x.AvailabilityModeId == availabilityModeId) 
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetByAvailabilityModeAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetBySpecialtyAsync(int specialtyId)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.Where(x => x.SpecialtyID == specialtyId).ToListAsync();
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetBySpecialtyAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetByLicenseNumberAsync(string licenseNumber)
        {
            OperationResult result = new OperationResult();
            try
            {
               
                result.data = await _context.Doctors.Where(x => x.LicenseNumber == licenseNumber).ToListAsync();
                if (result.data == null )
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetByLicenseNumberAsync"];
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
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetActiveDoctorsAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsByExperienceAsync(int minYears, int maxYears)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.Where(x => x.YearsOfExperience >= minYears && x.YearsOfExperience <= maxYears).ToListAsync();
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetDoctorsByExperienceAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsByConsultationFeeAsync(decimal minFee, decimal maxFee)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.Where(x => x.ConsultationFee >= minFee && x.ConsultationFee <= maxFee).ToListAsync();
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetDoctorsByConsultationFeeAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetDoctorsWithExpiringLicenseAsync(DateOnly expirationDate)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Doctors.Where(x => x.LicenseExpirationDate == expirationDate).ToListAsync();
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetDoctorsWithExpiringLicenseAsync"];
            }
            return result;
        }

        public override async Task<OperationResult> SaveEntityAsync(Doctors doctors)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (doctors == null)
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataDoctorsIsNull"];
                    return result;
                }


                _context.Doctors.Add(doctors);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
           
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.SaveEntityErrorMessage["SaveEntityError"];
                result.Success = false;
                _logger.LogError(ex, "Error while saving Doctor.");
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
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["UpdateEntityAsync"];
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
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetAllAsync"];
            }
            return result;
        }


        public override async Task<OperationResult> GetAllAsync()
        {
             OperationResult result = new OperationResult();
            try
            {
                  
                var consult = await _context.Doctors.ToListAsync();

                result.data = consult;

           }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetAllAsync"];
                _logger.LogError(_repErrorMapper.SaveEntityErrorMessage + ex.Message.ToString());
            }

             return result;
        }

        public override async Task<bool> ExistsAsync(Expression<Func<Doctors, bool>> filter)
        {
            return await _context.Doctors.AnyAsync(filter);

        }

        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var doctor = await _context.Doctors.FindAsync(id);
                if (doctor == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];

                    result.Success = false;
                }
                else
                {
                    result.data = doctor;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorDoctorsRepositoryMessages["GetAllAsync"];
            }
            return result;
        }



    }
}
