
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Repositories
{
    public class PatientsRepository : BaseRepository<Patients>, IPatientsRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<PatientsRepository> _logger;
        private readonly IRepErrorMapper _repErrorMapper;
        public PatientsRepository(NetMedContext context,
            ILogger<PatientsRepository> logger,
            IRepErrorMapper repErrorMapper)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _repErrorMapper = repErrorMapper;
        }

        public async Task<OperationResult> GetByBloodTypeAsync(string bloodType)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(bloodType);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetByBloodTypeAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetByInsuranceProviderAsync(int providerId)
        {
            OperationResult result = new OperationResult();
            
            try
            { 
                result.data = await _context.Patients.FindAsync(providerId);

            
                if (result.data == null)
                {
                    result.Success = false;
                    result.Message  = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                }
                result.Message = _repErrorMapper.SuccessPatientsRepositoryMessages["GetByInsuranceProviderAsync"];
            }
            catch (Exception )
            {
                result.Success = true;
               
            }
            return  result;
        }
            public async Task<OperationResult> GetPatientsAsyncWithoutInsuranceAsync()
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.FindAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsAsyncWithoutInsuranceAsync"];
            }
            return result;
        }

        public async Task<OperationResult> SearchByAddressAsync(string addressFragment)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.FindAsync(addressFragment);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["SearchByAdress"];
            }
            return result;
        }

        public async Task<OperationResult> GetPatientsByAgeRangeAsync(int minAge, int maxAge)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                }
                result.data = await _context.Patients.FindAsync(minAge, maxAge);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsByAgeRangeAsync"];
            }

            return result;
        }

        public async Task<OperationResult> GetByEmergencyContactAsync(string contactInfo)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(contactInfo);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetByEmergencyContactAsync"];
            }
            return result;
        }
        public async Task<OperationResult> GetPatientsWithAllergiesAsync(string? allergy = null)
        {
            OperationResult result = new OperationResult();
            try
            {
              if(result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }   

                result.data = await _context.Patients.FindAsync(allergy);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsWithAllergiesAsync"];
            }
            return result;
        }

        public async Task<OperationResult> GetPatientsByGenderAsync(string gender)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (result.data == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                result.data = await _context.Patients.FindAsync(gender);
             
                result.Success = true;
            }
            catch
        (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsByGenderAsync"];
            }
            return result;
        }
        public override async Task<OperationResult> SaveEntityAsync(Patients patients)
        {
           
            OperationResult result = new OperationResult();
            try
            {
                if (patients == null)
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    return result;
                }
                _context.Patients.Add(patients);
                await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.SaveEntityErrorMessage["SaveEntityError"];
                result.Success = false;
                _logger.LogError(ex, _repErrorMapper.SaveEntityErrorMessage["SaveEntityError"]);
            }
            return result;
        }


        public override async Task<OperationResult> UpdateEntityAsync(Patients patients)
        {

            OperationResult result = new OperationResult();
            try
            {
                _context.Patients.Update(patients);
                 await _context.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message + _repErrorMapper.UpdateEntityErrorMessage["UpdateEntityAsync"];
                result.Success = false;
            }
            return result;
        }

        public override async Task<OperationResult> GetAllAsync(Expression<Func<Patients, bool>> filter)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.Where(filter).ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetEntityByIdErrorMessage["GetAllEntitiesError"];
            }
            return result;

        }
        public override async Task<OperationResult> GetEntityByIdAsync(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var patient = await _context.Patients.FindAsync(id);
                if (patient == null)
                {
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    result.Success = false;
                }
                else
                {
                    result.data = patient;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetEntityByIdErrorMessage["GetEntityByIdError"];
            }
            return result;
        }



        public override async Task<bool> ExistsAsync(Expression<Func<Patients, bool>> filter)
        {
            return await _context.Patients.AnyAsync(filter);
        }

        public override async Task<OperationResult> GetAllAsync()
        {
         OperationResult result = new OperationResult();
            try
            {
                result.data = await _context.Patients.ToListAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.GetAllEntitiesErrorMessage["GetAllEntitiesError"];
                _logger.LogError(ex.Message);
            }
            return result;
        }

    }




}

