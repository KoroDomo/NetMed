using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Users;

namespace WebApplicationRefactor.Application.Contracts
{
    public interface IUsersSevice : IBaseAppService<UsersApiModel, UsersApiModel, UsersApiModel>

    {

    }
}
