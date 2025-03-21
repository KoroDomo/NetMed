
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

        public async Task<OperationResult> GetByBloodTypeAsync(char bloodType)
        {
            OperationResult result = new OperationResult();
            try
            {
                // Use Where/FirstOrDefaultAsync to find by non-primary key properties
                var patients = await _context.Patients
                    .Where(p => p.BloodType == bloodType)
                    .ToListAsync();

                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = "Tipo de sangre encontrado";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                if (_repErrorMapper?.ErrorPatientsRepositoryMessages != null && _repErrorMapper.ErrorPatientsRepositoryMessages.ContainsKey("GetByBloodTypeAsync"))
                {
                    result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetByBloodTypeAsync"];
                }
                else
                {
                    result.Message = ex.Message + " An error occurred in GetByBloodTypeAsync.";
                }
            }

            return result;
        }

        public async Task<OperationResult> GetByInsuranceProviderAsync(int providerId)
        {
            OperationResult result = new OperationResult();

            try
            {
                // Use Where clause instead of FindAsync for non-primary key searches
                var patients = await _context.Patients
                    .Where(p => p.InsuranceProviderID == providerId)
                    .ToListAsync();

                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    if (_repErrorMapper?.DataISNullErrorGlogal?["DataIsNull"] == null)
                    {
                        result.Message = "Error: DataIsNullErrorGlogal is not set.";
                    }
                    else
                    {
                        result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    }
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = "Proveedor de seguro encontrado";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["GetByInsuranceProviderAsync"];
            }

            return result;
        }

        public async Task<OperationResult> SearchByAddressAsync(string address)
        {
            OperationResult result = new OperationResult();
            try
            {
                var patients = await _context.Patients
                    .Where(p => p.Address == address)
                    .ToListAsync();

                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    if (_repErrorMapper?.DataISNullErrorGlogal?["DataIsNull"] == null)
                    {
                        result.Message = "Error: DataIsNullErrorGlogal is not set.";
                    }
                    else
                    {
                        result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                    }
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = "Direccion del paciente encontrada";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message + _repErrorMapper.ErrorPatientsRepositoryMessages["SearchByAdress"];
            }
            return result;
        }

        public async Task<OperationResult> GetByEmergencyContactAsync(string EcontactInfo)
        {
            OperationResult result = new OperationResult();
            try
            {
                var patients = await _context.Patients
                    .Where(p => p.EmergencyContactName == EcontactInfo)
                    .ToListAsync();
                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = "Contacto de emergencia encontrado";
                }
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
                var query = _context.Patients.AsQueryable();

                if (!string.IsNullOrEmpty(allergy))
                {
                    query = query.Where(p => p.Allergies != null && p.Allergies.Contains(allergy));
                }
                else
                {
                    query = query.Where(p => p.Allergies != null && p.Allergies != "");
                }

                var patients = await query.ToListAsync();

                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsWithAllergiesAsync"];
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = _repErrorMapper.SuccessPatientsRepositoryMessages["GetPatientsWithAllergiesAsync"];
                }
            }
            catch (Exception ex)
            {
                result.Success = false;

                string errorMessage = ex.Message;
                if (_repErrorMapper != null &&
                    _repErrorMapper.ErrorPatientsRepositoryMessages != null &&
                    _repErrorMapper.ErrorPatientsRepositoryMessages.ContainsKey("GetPatientsWithAllergiesAsync"))
                {
                    errorMessage += _repErrorMapper.ErrorPatientsRepositoryMessages["GetPatientsWithAllergiesAsync"];
                }

                result.Message = errorMessage;
            }

            return result;
        }

        public async Task<OperationResult> GetPatientsByGenderAsync(char gender)
        {
            OperationResult result = new OperationResult();
            try
            {
                var patients = await _context.Patients
                    .Where(p => p.Gender == gender)
                    .ToListAsync();
                if (patients == null || !patients.Any())
                {
                    result.Success = false;
                    result.Message = _repErrorMapper.DataISNullErrorGlogal["DataIsNull"];
                }
                else
                {
                    result.data = patients;
                    result.Success = true;
                    result.Message = "Pacientes encontrados por género";
                }
            }
            catch (Exception ex)
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

