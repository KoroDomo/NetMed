
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Model.Models;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Repositories
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<MedicalRecordsRepository> logger;
        private readonly IConfiguration configuration;

        public MedicalRecordsRepository(NetMedContext context, ILogger<MedicalRecordsRepository> logger, IConfiguration configuration)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            var result = new OperationResult();
            try
            {
                //  Verificar que el paciente exista antes de guardar el registro
                bool patientExists = true; // Aun no tengo acceso a la entidad paciente para implementar esta validacion correctamente
                if (!patientExists)
                {
                    result.Message = "PatientID invalido";
                    result.Success = false;
                    return result;
                }

                await base.SaveEntityAsync(entity);
                result.Success = true;
                result.Message = "Record Medico guardado";
            }
            catch (Exception ex)
            {
                result.Message = "Error guardando Record Medico.";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetMedicalRecordByIdAsync(int recordId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var record = await context.MedicalRecords
                    .Where(r => r.Id == recordId)
                    .Select(r => new MedicalRecordsModel
                    {
                        Id = r.Id,
                        PatientId = r.PatientID,
                        Diagnosis = r.Diagnosis,
                        Treatment = r.Treatment,
                        DateCreated = r.DateCreated
                    })
                    .FirstOrDefaultAsync();

                if (record != null)
                {
                    result.Data = record;
                    result.Success = true;
                }
                else
                {
                    result.Message = "Record Medico no encontrado";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorMedicalRecordsRepository:GetMedicalRecordById"] ?? "Error al solicitar el Record MEdico";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override async Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            var result = new OperationResult();
            try
            {
                var existingRecord = await context.MedicalRecords.FindAsync(entity.Id);
                if (existingRecord == null)
                {
                    result.Message = "Record Medico no encontrado";
                    result.Success = false;
                    return result;
                }

                existingRecord.Diagnosis = entity.Diagnosis;
                existingRecord.Treatment = entity.Treatment;
                existingRecord.DateOfVisit = entity.DateOfVisit;

                await base.UpdateEntityAsync(existingRecord);
                result.Success = true;
                result.Message = "Record Medico guartdado";
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Record";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetByTreatmentAsync(string treatment)
        {
            var result = new OperationResult();
            try
            {
                var records = await context.MedicalRecords
                    .Where(r => r.Treatment.Contains(treatment))
                    .Select(r => new MedicalRecordsModel
                    {
                        Id = r.Id,
                        PatientId = r.PatientID,
                        Diagnosis = r.Diagnosis,
                        Treatment = r.Treatment,
                        DateCreated = r.DateCreated
                    })
                    .ToListAsync();

                if (records.Any())
                {
                    result.Data = records;
                    result.Success = true;
                }
                else
                {
                    result.Message = "No se encuentra un Record Medico on el Tratamiento indicado";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error solicitando Record con este tratamiento";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<OperationResult> GetLatestByPatientIdAsync(int patientId)
        {
            var result = new OperationResult();
            try
            {
                var latestRecord = await context.MedicalRecords
                    .Where(r => r.PatientID == patientId)
                    .OrderByDescending(r => r.DateOfVisit)
                    .Select(r => new MedicalRecordsModel
                    {
                        Id = r.Id,
                        PatientId = r.PatientID,
                        Diagnosis = r.Diagnosis,
                        Treatment = r.Treatment,
                        DateCreated = r.DateCreated
                    })
                    .FirstOrDefaultAsync();

                if (latestRecord != null)
                {
                    result.Data = latestRecord;
                    result.Success = true;
                }
                else
                {
                    result.Message = "No se encontro Record Medico con este paciente";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error solicitando Record MEdico con este PatientID.";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}