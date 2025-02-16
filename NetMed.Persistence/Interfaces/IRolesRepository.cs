
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Domain.Repository;
using NetMed.Persistence.Base;
using System.Data;

namespace NetMed.Persistence.Context.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {
        Task<OperationResult> GetAllRolesAsync();

        Task<OperationResult> GetRoleByIdAsync(int roleId);

        Task<OperationResult> CreateRoleAsync(Roles role);

        Task<OperationResult> UpdateRoleAsync(Roles role);

        Task<OperationResult> DeactivateRoleAsync(int roleId);

    }
}
