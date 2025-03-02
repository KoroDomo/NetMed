
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Domain.Base;
using Microsoft.Extensions.Configuration;
using NetMed.Model.Models;

namespace NetMed.Persistence.Repositories
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<MedicalRecordsRepository> _logger;
        private readonly IConfiguration _configuration;

        public MedicalRecordsRepository(NetMedContext context, ILogger<MedicalRecordsRepository> logger, IConfiguration configuration)
            : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "El Record Medico no puede ser nulo";
                    return result;
                }
                if (entity.PatientID == 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no puede ser 0";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Diagnosis))
                {
                    result.Success = false;
                    result.Message = "El diagnostico no puede ser nulo";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Treatment))
                {
                    result.Success = false;
                    result.Message = "El tratamiento no puede ser nulo";
                    return result;
                }
                await base.SaveEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error guardando el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "El Record Medico no puede ser nulo";
                    return result;
                }
                if (entity.PatientID == 0)
                {
                    result.Success = false;
                    result.Message = "El ID del paciente no puede ser 0";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Diagnosis))
                {
                    result.Success = false;
                    result.Message = "El diagnostico no puede ser nulo";
                    return result;
                }
                if (string.IsNullOrEmpty(entity.Treatment))
                {
                    result.Success = false;
                    result.Message = "El tratamiento no puede ser nulo";
                    return result;
                }
                await base.UpdateEntityAsync(entity);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error actualizando el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetMedicalRecordByIdAsync(int recordId)
        {
            OperationResult result = new OperationResult();
            try
            {
                result.Data = await (from record in _context.MedicalRecords
                                     where record.Id == recordId
                                     && record.IsActive == true
                                     select new MedicalRecordsModel()
                                     {
                                         PatientId = record.PatientID,
                                         Diagnosis = record.Diagnosis,
                                         Treatment = record.Treatment,
                                         DateOfVisit = record.DateOfVisit,
                                         CreatedAt = record.CreatedAt,
                                         UpdatedAt = record.UpdatedAt,
                                         RecordID = record.Id,
                                         DoctorID = record.DoctorID
                                     }).AsNoTracking()
                               .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetByTreatmentAsync(string treatmentName)
        {
            OperationResult result = new OperationResult();
            try
            {
                var treatment = await _context.MedicalRecords.FirstOrDefaultAsync
                    (t => t.Treatment == treatmentName);

                if (treatment == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró el Record Medico";
                    return result;
                }
                result.Data = treatment;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetLatestByPatientIdAsync(int patientId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var patient = await _context.MedicalRecords.FirstOrDefaultAsync
                    (p => p.PatientID == patientId);
                if (patient == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró el Record Medico";
                    return result;

                }
                result.Data = patient;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo el Record Medico";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
