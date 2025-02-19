
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Persistence.Base;
using System.Data;

namespace NetMed.Persistence.Context.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {

        Task<OperationResult> GetRoleByIdAsync(int rolesId);

        Task<OperationResult> CreateRoleAsync(Roles roles);

        Task<OperationResult> UpdateRoleAsync(Roles roles);

        Task<OperationResult> DeleteRoleAsync(int roles);

    }
}
