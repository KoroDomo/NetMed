using NetMedWebApi.Models;
using NetMedWebApi.Models.Roles;

namespace NetMedWebApi.Persistence.Interfaces
{
    public interface IRolesRepository
    {
        Task<OperationResultList<RolesApiModel>> GetAllRolesAsync();

        Task<OperationResult<T>> GetRoleByIdAsync<T>(int Id);

        Task<OperationResult<SaveRolesModel>> CreateRoleAsync(SaveRolesModel model);

        Task<OperationResult<UpdateRolesModel>> UpdateRoleAsync(UpdateRolesModel model);

        Task<OperationResult<DeleteRolesModel>> DeleteRoleAsync(DeleteRolesModel model);
    }
}

