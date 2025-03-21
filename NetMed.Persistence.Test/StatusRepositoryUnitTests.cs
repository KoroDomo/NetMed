using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Loggin.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;


public class StatusRepositoryTests
{
    private readonly NetmedContext _context;
    private readonly StatusRepository _statusRepository;
    private readonly ILoggerCustom _logger;
    private readonly JsonMessage _jsonMessage;

    public StatusRepositoryTests()
    {
        var opinios = new DbContextOptionsBuilder<NetmedContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                  .ToString())
                  .Options;

        _context = new NetmedContext(opinios);

        _jsonMessage = new JsonMessage();

        var FactoryLogger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
        });

        _logger = new LoggerCustom(FactoryLogger.CreateLogger<LoggerCustom>());

        _statusRepository = new StatusRepository(_context, _logger, _jsonMessage);
    }



        [Fact]
    public async Task CreateStatusAsync_WhenStatusIsValid_ReturnsSuccess()
    {
        var status = new Status { Id = 1, StatusName = "Active" };

        var result = await _statusRepository.CreateStatusAsync(status);

       
        Assert.True(result.Success);
        Assert.Equal(_jsonMessage.SuccessMessages["EntityCreated"], result.Message);
    }

    [Fact]
    public async Task CreateStatusAsync_WhenStatusIsNotValid_ReturnsFailed()
    {
        var status = new Status { Id = -1, StatusName = "Active" }; 

        var result = await _statusRepository.CreateStatusAsync(status);

        Assert.False(result.Success);
        Assert.Equal(_jsonMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task GetStatusByIdAsync_WhenStatusIdIsValid_ReturnsStatus()
    {
        var status = new Status { Id = 1, StatusName = "Active" };
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        var result = await _statusRepository.GetStatusByIdAsync(status.Id);

        Assert.True(result.Success);
        Assert.Equal(_jsonMessage.SuccessMessages["StatusFound"], result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task GetStatusByIdAsync_WhenStatusIdIsNotValid_ReturnsFailed()
    {
        var status = new Status { Id = -1, StatusName = "Active" }; 
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        var result = await _statusRepository.GetStatusByIdAsync(status.Id);

        Assert.False(result.Success);
        Assert.Equal(_jsonMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task UpdateStatusAsync_WhenStatusIsValid_ReturnsSuccess()
    {
        var status = new Status { Id = 1, StatusName = "Active" };
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        status.StatusName = "Inactive";
        var result = await _statusRepository.UpdateStatusAsync(status);

        Assert.True(result.Success);
        Assert.Equal(_jsonMessage.SuccessMessages["EntityUpdated"], result.Message);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task UpdateStatusAsync_WhenStatusIsNotValid_ReturnsFailed()
    {
        var status = new Status { Id = -1, StatusName = "Active" }; 
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        status.StatusName = "Inactive";
        var result = await _statusRepository.UpdateStatusAsync(status);

        Assert.False(result.Success);
        Assert.Equal(_jsonMessage.ErrorMessages["InvalidId"], result.Message);
    }

    [Fact]
    public async Task DeleteStatusAsync_WhenStatusIsValid_ReturnsSuccess()
    {
        var status = new Status { Id = 1, StatusName = "Active" };
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        var result = await _statusRepository.DeleteStatusAsync(status.Id);

        Assert.True(result.Success);
        Assert.Equal(_jsonMessage.SuccessMessages["EntityDeleted"], result.Message);
    }

    [Fact]
    public async Task DeleteStatusAsync_WhenStatusIsNotValid_ReturnsFailed()
    {
        var status = new Status { Id = -1, StatusName  = "Active" }; 
        _context.statuses.Add(status);
        await _context.SaveChangesAsync();

        var result = await _statusRepository.DeleteStatusAsync(status.Id);

      
        Assert.False(result.Success);
        Assert.Equal(_jsonMessage.ErrorMessages["InvalidId"], result.Message);
    }
}