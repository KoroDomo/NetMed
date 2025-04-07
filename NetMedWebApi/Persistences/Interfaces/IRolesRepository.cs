using NetMed.Domain.Base;
using NetMed.Domain.Entities;


namespace NetMedWebApi.Persistence.Interfaces
{
    public interface IRolesRepository
    {

        Task<OperationResult> GetRoleByIdAsync(int roles);

        Task<OperationResult> CreateRoleAsync(Roles roles);

        Task<OperationResult> UpdateRoleAsync(Roles roles);

        Task<OperationResult> DeleteRoleAsync(int roles);

    }
}
