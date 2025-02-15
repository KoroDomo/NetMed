
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NetMed.Persistence.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Context;
using NetMed.Model.Models;
using NetMed.Domain.Base;

using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace NetMed.Persistence.Repositories
{
    public class MedicalRecordsRepository : BaseRepository<MedicalRecords>, IMedicalRecordsRepository
    {
        private readonly NetMedContext context;
        private readonly ILogger<MedicalRecordsRepository> logger;

        public MedicalRecordsRepository(NetMedContext context, ILogger<MedicalRecordsRepository> logger)
            : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public override async Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Record Medico");
            if (!validation.Success) return validation;

            // Validacion pentiente, ya que no tengo acceso a la entidad Patient aun
            bool patientExists = true;
            if (!patientExists)
            {
                return new OperationResult { Success = false, Message = "PatientID invalido" };
            }

            await base.SaveEntityAsync(entity);
            return new OperationResult { Success = true, Message = "Record MEdico guardado" };
        }

        public override async Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            var validation = EntityValidator.ValidateNotNull(entity, "Record Medico");
            if (!validation.Success) return validation;

            var existingRecord = await context.MedicalRecords.FindAsync(entity.Id);
            if (existingRecord == null)
            {
                return new OperationResult { Success = false, Message = "Record Medico no encontrado" };
            }

            existingRecord.Diagnosis = entity.Diagnosis;
            existingRecord.Treatment = entity.Treatment;
            existingRecord.DateOfVisit = entity.DateOfVisit;

            await base.UpdateEntityAsync(existingRecord);
            return new OperationResult { Success = true, Message = "Record Medico actualizado" };
        }

        public async Task<OperationResult> GetMedicalRecordByIdAsync(int recordId)
        {
            var record = await context.MedicalRecords
                .Where(r => r.Id == recordId)
                .FirstOrDefaultAsync();

            return record != null
                ? new OperationResult { Success = true, Data = record }
                : new OperationResult { Success = false, Message = "Record Medico no encontrado" };
        }

        public async Task<OperationResult> GetByTreatmentAsync(string treatment)
        {
            var records = await context.MedicalRecords
                .Where(r => r.Treatment.Contains(treatment))
                .ToListAsync();

            return records.Any()
                ? new OperationResult { Success = true, Data = records }
                : new OperationResult { Success = false, Message = "No se encontro un Record Medico con este tratamiento" };
        }

        public async Task<OperationResult> GetLatestByPatientIdAsync(int patientId)
        {
            var latestRecord = await context.MedicalRecords
                .Where(r => r.PatientID == patientId)
                .OrderByDescending(r => r.DateOfVisit)
                .FirstOrDefaultAsync();

            return latestRecord != null? 
                new OperationResult { Success = true, Data = latestRecord }
                : new OperationResult { Success = false, Message = "No se encontro un Record Medico para este paciente" };
        }
    }
}
