using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Persistence.Test;
public class UnitTestUsers
{
    private readonly NetMedContext _context;
    private readonly UsersRepository _usersRepository;
    private readonly RepErrorMapper repErrorMapper;

    public UnitTestUsers()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        var logger = new Mock<ILogger<UsersRepository>>().Object;
        repErrorMapper = new RepErrorMapper();
        _usersRepository = new UsersRepository(_context, logger, repErrorMapper);
    }

    [Fact]
    public async Task GetActiveUsersAsyncReturnsActiveUsers()
    {
        var users = new Users
        {
            UserId = 1,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "rrr@gmail.com",
            Password = "1234",
            RoleID = 0,



        };

        var result = await _usersRepository.GetActiveUsersAsync();
        Assert.NotNull(result);
        Assert.True(result.Success);
       
    }

    [Fact]
    public async Task GetEmailAsyncReturnsEmail()
    {

        var users = new Users
        {
            UserId = 1,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "rr@ffaf",
            Password = "1234",
            RoleID = 0,
        };

        var result = await _usersRepository.GetEmailAsync("rr@ffaf");
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Correo disponible", result.Message);
    }

    [Fact]
    public async Task GetByRoleByIDAsyncReturnsRole()
    {

        var users = new Users
        {
            UserId = 1,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "rr@ffaf",
            Password = "1234",
            RoleID = 2,
        };

        var result = await _usersRepository.GetByRoleByIDAsync(2);

        Assert.NotNull(result);
        Assert.True(result.Success);


    }

    [Fact]
    public async Task SearchByNameAsyncReturnsName()
    {

        var users = new Users
        {
            UserId = 1,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "rr@ffaf",
            Password = "1234",
            RoleID = 0,
        };
        var result = await _usersRepository.SearchByNameAsync("Juan", "Perez");
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task PasswordReturnAsyncReturnsPassword()
    {
        var users = new Users
        {
            UserId = 1,
            FirstName = "Juan",
            LastName = "Perez",
            Email = "rr@ffaf",
            Password = "1234",
            RoleID = 0,
        };

    _context.Users.Add(users);
         await _context.SaveChangesAsync();
        var result = await _usersRepository.GetPasswordAsync("1234");
        Assert.NotNull(result);
        Assert.True(result.Success);
    }



}

