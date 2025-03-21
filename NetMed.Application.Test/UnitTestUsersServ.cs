using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Application.Services;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Test;


public class UnitTestUsersServ
{
    private readonly Mock<IUsersRepository> _mockUsersRepository;
    private readonly ILogger<UsersServices> _logger;
    private readonly IUsersServices _usersServices;

    public UnitTestUsersServ()
    {
        _mockUsersRepository = new Mock<IUsersRepository>();
        _logger = new Mock<ILogger<UsersServices>>().Object;
        _usersServices = new UsersServices(_mockUsersRepository.Object, _logger);
    }

    [Fact]
    public async Task GetAllUsersReturnAllUsersData()
    {
        // Arrange
        var users = new List<Users>
        {
            new Users { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "33431" },
            new Users { UserId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "33431" }
        };

        var operationResult = new OperationResult
        {
            Success = true,
            Message = "Users retrieved successfully",
            data = users
        };

        _mockUsersRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(operationResult);

        // Act
        var result = await _usersServices.GetAllData();

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Users retrieved successfully", result.Message);
        var resultUsers = result.data as List<Users>;
        Assert.NotNull(resultUsers);
        Assert.Equal(2, resultUsers.Count);
        Assert.Equal("John", resultUsers[0].FirstName);
        Assert.Equal("Jane", resultUsers[1].FirstName);
    }

    [Fact]
    public async Task GetUserByIdReturnUserData()
    {
        // Arrange
        int userId = 1;
        var user = new Users { UserId = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "33431" };

        var operationResult = new OperationResult
        {
            Success = true,
            Message = "User found",
            data = user
        };

        _mockUsersRepository.Setup(repo => repo.GetEntityByIdAsync(userId))
            .ReturnsAsync(operationResult);

        // Act
        var result = await _usersServices.GetById(userId);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User found", result.Message);
        var resultUser = result.data as Users;
        Assert.NotNull(resultUser);
        Assert.Equal(userId, resultUser.UserId);
        Assert.Equal("John", resultUser.FirstName);
    }

    [Fact]
    public async Task AddUsersReturnAddedUser()
    {
        // Arrange
        var newUser = new AddUserDto { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", Password = "password123" };
        var createdUser = new Users { UserId = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", Password = "password123" };

        var operationResult = new OperationResult
        {
            Success = true,
            Message = "User added successfully",
            data = createdUser
        };

        _mockUsersRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Users>()))
            .ReturnsAsync(operationResult);

        // Act
        var result = await _usersServices.Add(newUser);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User added successfully", result.Message);
        var resultUser = result.data as Users;
        Assert.NotNull(resultUser);
        Assert.Equal(3, resultUser.UserId);
        Assert.Equal("Alice", resultUser.FirstName);
    }

    [Fact]
    public async Task UpdateUsersReturnUpdatedUser()
    {
        // Arrange
        var userToUpdate = new UpdateUserDto { UserId = 1, FirstName = "John", LastName = "Updated", Email = "john@example.com", Password = "33431" };

        var operationResult = new OperationResult
        {
            Success = true,
            Message = "User updated successfully",
            data = new Users { UserId = 1, FirstName = "John", LastName = "Updated", Email = "john@example.com", Password = "33431" }
        };

        _mockUsersRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Users>()))
            .ReturnsAsync(operationResult);

        // Act
        var result = await _usersServices.Update(userToUpdate);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User updated successfully", result.Message);
        var resultUser = result.data as Users;
        Assert.NotNull(resultUser);
        Assert.Equal("Updated", resultUser.LastName);
    }

    [Fact]
    public async Task DeleteUsersReturnDeletedUser()
    {
        // Arrange
        int userId = 1;
        var deletedUser = new Users { UserId = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com", Password = "33431" };

        var operationResult = new OperationResult
        {
            Success = true,
            Message = "User deleted successfully",
            data = deletedUser
        };

        _mockUsersRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Users>()))
            .ReturnsAsync(operationResult);

        var deleteUserDto = new DeleteUserDto
        {
            UserId = userId,
            Deleted = true,
            FirstName = "John",
            LastName = "Doe",
            Password = "33431"
        };

        // Act
        var result = await _usersServices.Delete(deleteUserDto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User deleted successfully", result.Message);
        var resultUser = result.data as Users;
        Assert.NotNull(resultUser);
        Assert.Equal(userId, resultUser.UserId);
    }
}
