using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NPOI.SS.Formula.Functions;

namespace NetMed.Persistence.Test
{
    public class NetworkTypeRepositoryUnitTest
    {
        private readonly NetMedContext _context;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly INetworkTypeRepository _repository;

        public NetworkTypeRepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new NetMedContext(options);

            _mockLogger = new Mock<ICustomLogger>();
            _repository = new NetworkTypeRepository(_context, _mockLogger.Object);
        }

        [Fact] //SaveEntityAsync
        public async Task SaveEntityAsync_ValidNetwork_ReturnsSuccess()
        {
            // Arrange
            var network = new NetworkType
            {
                Id = 1,
                Name = "Network",
                Description = "Description",
                IsActive = true

            };

            // Act
            var result = await _repository.SaveEntityAsync(network);
            await _context.SaveChangesAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(await _repository.GetNetworkTypeById(network.Id));
            Assert.Contains("Registro guardado exitosamente.", result.Message);

        }

        [Fact]
        public async Task SaveEntityAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var network = new NetworkType
            {
                
                Name = "ExistingNetwork",
                Description = "Description",
                IsActive = true

            };

            _context.NetworkType.Add(network);
            await _context.SaveChangesAsync();

            var newNetwork = new NetworkType { Name = "ExistingNetwork" };

            // Act
            var result = await _repository.SaveEntityAsync(newNetwork);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ya existe un NetworkType con este nombre.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidName_ReturnsError()
        {
            // Arrange
            var network = new NetworkType
            {
                Name = "ExistingNetwRETTTTTTTTTTTTTTrtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttTTTTTRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRfgggggggggggggggggggggggggggggggRRRRRRRork",
                Description = "Description",
                IsActive = true
            };

            // Act
            var result = await _repository.SaveEntityAsync(network);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("El campo Name excede la longitud maxima permitida.", result.Message);
        }

        [Fact] //GetNetworkTypeById
        public async Task GetNetworkTypeById_ShouldReturnProvider_WhenProviderExists()
        {
            // Arrange
            int networkId = 3;
            var network = new NetworkType
            {
                Id = networkId,
                Name = "Network2",
                Description = "Description",
                IsActive = true

            };

            _context.NetworkType.Add(network);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetNetworkTypeById(networkId);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(networkId, result.Result[0].Id);
            Assert.Equal("Network2", result.Result[0].Name);
            Assert.Contains("Network/s obtenido exitosamente.", result.Message);

        }

        [Fact]
        public async Task GetNetworkTypeById_ShouldReturnNotFound_WhenNetworkDoesNotExist()
        {
            // Arrange
            
            int networkId = 999;
            var network = new NetworkType
            {
                Id = 4,
                Name = "Network2",
                Description = "Description",
                IsActive = true

            };

            // Act
            var result = await _repository.GetNetworkTypeById(networkId);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.Equal("Registro no encontrado.", result.Message);
        }

        [Fact] //GettAllAsync
        public async Task GetAllAsync_ReturnsNetworks()
        {
            // Arrange
            var network1 = new NetworkType
            {
                Id = 5,
                Name = "Network2",
                Description = "Description",
                IsActive = true

            };

            _context.NetworkType.Add(network1);

            var network2 = new NetworkType
            {
                Id = 6,
                Name = "Network3",
                Description = "Description",
                IsActive = true

            };

            _context.NetworkType.Add(network2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result);
            Assert.Contains("Network/s obtenido exitosamente.", result.Message);
        }

        [Fact]//UpdateEntityAsync
        public async Task UpdateEntityAsync_ValidNetwork_ReturnsSuccess()
        {
            // Arrange
            var network = new NetworkType
            {
                Id = 7,
                Name = "Network4",
                Description = "Description",
                IsActive = true

            };
            _context.NetworkType.Add(network);
            await _context.SaveChangesAsync();


            network.Name = "Network Actualizado";

            // Act
            var result = await _repository.UpdateEntityAsync(network);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Network Actualizado", ((NetworkType)result.Result).Name);
            Assert.Contains("Registro actualizado exitosamente.", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_NonExistentNetwork_ReturnsError()
        {
            // Arrange
            var network = new NetworkType
            {
                Id = 8,
                Name = "Network5",
                Description = "Description",
                IsActive = true

            };

            // Act
            var result = await _repository.UpdateEntityAsync(network);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Registro no encontrado.", result.Message);
        }

        [Fact] //RemoveNetworkTypeAsync
        public async Task RemoveNetworkTypeAsync_ValidId_DeactivatesProvider()
        {
            // Arrange
            var network = new NetworkType
            {
                Id = 9,
                Name = "Network6",
                Description = "Description",
                IsActive = true

            };

            _context.NetworkType.Add(network);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.RemoveNetworkTypeAsync(9);

            // Assert
            Assert.True(result.Success);
            Assert.False(network.IsActive);
            Assert.Contains("Network eliminado exitosamente.", result.Message);
        }

        [Fact]
        public async Task RemoveNetworkTypeAsync_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _repository.RemoveNetworkTypeAsync(999);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Registro no encontrado.", result.Message);
        }

    }
}

    
