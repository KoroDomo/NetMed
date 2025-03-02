

using NetMed.Application.Base;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;

namespace NetMed.Application.Contracts
{
    public interface IUsersServices : IBaseService<AddUserDto, UpdateUserDto, DeleteUserDto >
    {

    }
}
