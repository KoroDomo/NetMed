using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Loggin.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;

public class RolesRepositoryTests
{
    private readonly NetmedContext _context;
    private readonly ILoggerCustom _Logger;

    private readonly IRolesRepository _rolesRepository;
    private readonly JsonMessage _messagedMessage;

    public RolesRepositoryTests()
    {
        var opinios = new DbContextOptionsBuilder<NetmedContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options;

        _context = new NetmedContext(opinios);

        _messagedMessage = new JsonMessage();

        var FactoryLogger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
        });

        _Logger = new LoggerCustom(FactoryLogger.CreateLogger<LoggerCustom>());

        _rolesRepository = new RolesRepository(_context, _Logger, _messagedMessage);

    }

    [Fact]
    public async Task CreateRoleAsync_WhenRoleIsValid_ReturnsSuccess()
    {
       
        var role = new Roles { Id = 1, RoleName = "Admin" };

        var result = await _rolesRepository.CreateRoleAsync(role);

        Assert.True(result.Success);
        Assert.Equal(_messagedMessage.SuccessMessages["RoleCreated"], result.Message);
    }

    [Fact]
    public async Task CreateRoleAsync_WhenRoleIsNotValid_ReturnsFailed()
    {
        
        var role = new Roles { Id = -1, RoleName = "Admin" }; 

        var result = await _rolesRepository.CreateRoleAsync(role);

        Assert.False(result.Success);
        Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task GetRoleByIdAsync_WhenRoleIdIsValid_ReturnsRole()
    {
        var role = new Roles { Id = 9, RoleName = "Manager" };
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _rolesRepository.GetRoleByIdAsync(role.Id);

        Assert.True(result.Success);
        Assert.Equal(_messagedMessage.SuccessMessages["RoleFound"], result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task GetRoleByIdAsync_WhenRoleIdIsNotValid_ReturnsFailed()
    {
        var role = new Roles { Id = -9, RoleName = "Manager" }; 
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _rolesRepository.GetRoleByIdAsync(role.Id);

    
        Assert.False(result.Success);
        Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task UpdateRoleAsync_WhenRoleIsValid_ReturnsSuccess()
    {
        var role = new Roles { Id = 43, RoleName = "Editor" };
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        role.RoleName = _messagedMessage.SuccessMessages["RoleUpdated"];
        var result = await _rolesRepository.UpdateRoleAsync(role);

        Assert.True(result.Success);
        Assert.Equal(_messagedMessage.SuccessMessages["RoleUpdated"], result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateRoleAsync_WhenRoleIsNotValid_ReturnsFailed()
    {
        var role = new Roles { Id = 43, RoleName = "Editor" };
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        role.Id = -1;
        var result = await _rolesRepository.UpdateRoleAsync(role);

        Assert.False(result.Success);
        Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task DeleteRoleAsync_WhenRoleIsValid_ReturnsSuccess()
    {
        var role = new Roles { Id = 43, RoleName = "Editor" };
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _rolesRepository.DeleteRoleAsync(role.Id);

        Assert.True(result.Success);
        Assert.Equal(_messagedMessage.SuccessMessages["RoleDeleted"], result.Message);
    }

    [Fact]
    public async Task DeleteRoleAsync_WhenRoleIsNotValid_ReturnsFailed()
    {
        var role = new Roles { Id = -43, RoleName = "Editor" }; 
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _rolesRepository.DeleteRoleAsync(role.Id);

        Assert.False(result.Success);
        Assert.Equal(_messagedMessage.ErrorMessages["InvalidId"], result.Message);
    }
}