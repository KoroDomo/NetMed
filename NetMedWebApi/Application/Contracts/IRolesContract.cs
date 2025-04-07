using NetMedWebApi.Application.Base;
using NetMedWebApi.Models.Roles;

namespace NetMedWebApi.Application.Contracts
{
    public interface IRolesContract : IBaseContract<RolesApiModel, SaveRolesModel, UpdateRolesModel, DeleteRolesModel>
    {
    }
}
