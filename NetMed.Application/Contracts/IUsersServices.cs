

using NetMed.Application.Base;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Application.Contracts
{
    public interface IUsersService : IBaseService<AddUserDto, UpdateUserDto, DeleteUserDto >
    {

    }
}
