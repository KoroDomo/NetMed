using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Domain.Base;

namespace NetMed.Persistence.Interfaces
{
    public interface IMedicalRecordsRepository : IBaseRepository<MedicalRecords>
    {
        Task<OperationResult> Treatment(string Treatment);
    }
}
