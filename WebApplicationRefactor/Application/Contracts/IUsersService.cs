using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Users;

namespace WebApplicationRefactor.Application.Contracts
{
    public interface IUsersService : IBaseAppService<UsersApiModel, UsersApiModel, UsersApiModel>

    {

    }
}
