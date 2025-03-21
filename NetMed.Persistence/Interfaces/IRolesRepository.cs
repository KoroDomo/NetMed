
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;


namespace NetMed.Persistence.Context.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {

        Task<OperationResult> GetRoleByIdAsync(int roles);

        Task<OperationResult> CreateRoleAsync(Roles roles);

        Task<OperationResult> UpdateRoleAsync(Roles roles);

        Task<OperationResult> DeleteRoleAsync(int roles);

    }
}
