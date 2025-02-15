
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

        public MedicalRecordsRepository(NetMedContext context, ILogger<MedicalRecordsRepository> logger, IConfiguration configuration) : base(context)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }

        public override Task<OperationResult> SaveEntityAsync(MedicalRecords entity)
        {
            // Agregar validaciones correspondientes antes de guardar la entidad
            return base.SaveEntityAsync(entity);
        }

        public async Task<OperationResult> GetMedicalRecordByIdAsync(int recordId)
        {
            OperationResult result = new OperationResult();
            try
            {
                var record = await context.MedicalRecords
                    .Where(r => r.Id == recordId)
                    .Select(r => new MedicalRecordsModel()
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
                    result.Message = "Medical record not found.";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = this.configuration["Messages:ErrorMedicalRecordsRepository:GetMedicalRecordById"] ?? "An error occurred while retrieving the medical record.";
                result.Success = false;
                this.logger.LogError(ex, result.Message);
            }
            return result;
        }

        public override Task<OperationResult> UpdateEntityAsync(MedicalRecords entity)
        {
            // Agregar validaciones correspondientes antes de actualizar la entidad
            return base.UpdateEntityAsync(entity);
        }

        public Task<OperationResult> Treatment(string Treatment)
        {
            throw new NotImplementedException();
        }
    }
}
