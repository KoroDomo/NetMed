using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Test;


public class UnitTestUsersServ
{
    private readonly Mock<IUsersServices> _mockUsersServices;
    private readonly Mock<UsersRepository> _mockUsersRepository;
    private NetMedContext _context;

    public UnitTestUsersServ()
    {
        var Options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _mockUsersServices = new Mock<IUsersServices>();
        _context = new NetMedContext(Options);
        _mockUsersRepository = new Mock<UsersRepository>(_context);


    }
    [Fact]
    public async Task GetAllUsersReturnAllUsersData()
    {
        var mockUsers = new OperationResult
        {
            Success = true,
            data = new List<AddUsersDto>
            {
                new AddUsersDto
                {
                    UserId = 1,
                    FirstName = "Juan",
                    LastName = "Perez",
                    Email = "jp@gmail.com",
                    Password = "1234",
                    RoleID = 0,
                },
        }

        };
        _mockUsersServices.Setup(x => x.GetAllData()).ReturnsAsync(mockUsers);
        var result = await _mockUsersServices.Object.GetAllData();
        Assert.True(result.Success);
        Assert.NotNull(result.data);

    }
    [Fact]
    public async Task GetUserByIdReturnUserData()
    {
        var mockUsers = new OperationResult
        {
            Success = true,
            data = new List<AddUsersDto>
            {
                new AddUsersDto{
                UserId = 1,
                FirstName = "Pedro",
                LastName = "Gomez",
                Email = "PG@gmail.com",
                Password = "1234",
                RoleID = 1,
            },
            }

        };

      
        _mockUsersServices.Setup(x => x.GetById(1)).ReturnsAsync(mockUsers);
        var result = await _mockUsersServices.Object.GetById(1);
        Assert.True(result.Success);
        Assert.NotNull(result.data);
    }
    [Fact]

    public async Task AddUsersReturnAddedUser()
    {
        var mockUsers = new OperationResult
        {
            Success = true,
            data = new AddUsersDto
            {
                UserId = 1,
                FirstName = "Cris",
                LastName = "Perez",
                Email = "CP@gmail.com",
                Password = "3203",
                RoleID = 2,
            },

        };

        var mockUsersDto = new AddUserDto
        {
            FirstName = "Cris",
            LastName = "Perez",
            Email = "BC@gmail.com",
            Password = "3203",
            RoleID = 2,
        };
        _mockUsersServices.Setup(x => x.Add(It.IsAny<AddUserDto>())).ReturnsAsync(mockUsers);
        var result = await _mockUsersServices.Object.Add(mockUsersDto);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdateUsersReturnUpdatedUser()
    {
        var mockUsers = new OperationResult
        {
            Success = true,
            data = new AddUsersDto
            {
                UserId = 1,
                FirstName = "Cris",
                LastName = "Perez",
                Email = "Jimena@gmail.com",
                Password = "0003",
                RoleID = 1,
            },

        };

        var mockUsersDto = new UpdateUserDto
        {
            UserId = 1,
            FirstName = "Cris",
            LastName = "Perez",
            Email = "Jimena@gmail.com",
            Password = "0003",
            RoleID = 1,
        };
        _mockUsersServices.Setup(x => x.Update(It.IsAny<UpdateUserDto>())).ReturnsAsync(mockUsers);
        var result = await _mockUsersServices.Object.Update(mockUsersDto);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task DeleteUsersReturnDeletedUser()
    {
        var mockUsers = new OperationResult
        {
            Success = true,
            data = new AddUsersDto
            {
                UserId = 1,
                FirstName = "Cris",
                LastName = "Perez",
                Email = "Suicide@hotmail.com",
                Password = "0001",
                RoleID = 1,
            },
        };
        var mockUsersDto = new DeleteUserDto { UserId = 1, Deleted = true, FirstName = "Cris", LastName = "Perez", Email = "Suicide@hotmail.com", Password = "0001", RoleID = 1, };
        _mockUsersServices.Setup(x => x.Delete(It.IsAny<DeleteUserDto>())).ReturnsAsync(mockUsers);
        var result = await _mockUsersServices.Object.Delete(mockUsersDto);

        Assert.NotNull(result);
        Assert.True(result.Success);
    }
}

