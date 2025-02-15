using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public interface IMedicalRecordsRepository : IBaseRepository<MedicalRecords>
    {
        Task<OperationResult> GetByTreatmentAsync(string treatment);

        // Obtiene el ultimo historial medico de un paciente
        Task<OperationResult> GetLatestByPatientIdAsync(int patientId);
    }
}
