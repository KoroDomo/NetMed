using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Domain.Base;
namespace NetMed.Persistence.Interfaces
{
    public interface ISpecialtiesRepository : IBaseRepository<Specialties>
    {
        Task<OperationResult> GetByNameAsync(string specialtyName);
    }
}
