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
       
    }
    [Fact]
    public async Task GetUserByIdReturnUserData()
    {
        
    }
    [Fact]

    public async Task AddUsersReturnAddedUser()
    {
    }
    [Fact]
    public async Task UpdateUsersReturnUpdatedUser()
    {
        
    }
    [Fact]
    public async Task DeleteUsersReturnDeletedUser()
    {
        
    }
}

