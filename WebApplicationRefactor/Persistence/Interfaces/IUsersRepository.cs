using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Users;
namespace NetMed.WebApplicationRefactor.Persistence.Interfaces
{ 
    public interface IUsersRepository : IBaseAppService<UsersApiModel, UsersApiModel, UsersApiModel>
    {

    }
    
}

