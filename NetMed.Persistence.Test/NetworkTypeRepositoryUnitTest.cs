using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Validators;
using NPOI.SS.Formula.Functions;

namespace NetMed.Persistence.Test
{
    public class NetworkTypeRepositoryUnitTest
    {
        private readonly NetMedContext _context;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly Mock<MessageMapper> _messageMapper;
        private readonly NetworkTypeValidator _validator;
        private readonly NetworkTypeRepository _repository;

        public NetworkTypeRepositoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new NetMedContext(options);

            _mockLogger = new Mock<ICustomLogger>();
            _messageMapper = new Mock<MessageMapper>();
            _validator = new NetworkTypeValidator(_messageMapper.Object);
            _repository = new NetworkTypeRepository(_context, _mockLogger.Object, _messageMapper.Object);
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

        [Fact] //GetInsurenProviderById
        public async Task GetInsurenProviderById_ShouldReturnProvider_WhenProviderExists()
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

        }

        [Fact]
        public async Task GetInsurenProviderById_ShouldReturnNotFound_WhenProviderDoesNotExist()
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
        public async Task GetAllAsync_ReturnsProviders()
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
            //Assert.Contains("Proveedores de seguros obtenidos exitosamente.", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_ValidProvider_ReturnsSuccess()
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
        }

        [Fact]
        public async Task UpdateEntityAsync_NonExistentProvider_ReturnsError()
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

        [Fact] //RemoveInsuranceProviderAsync
        public async Task RemoveInsuranceProviderAsync_ValidId_DeactivatesProvider()
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
        }

        [Fact]
        public async Task RemoveInsuranceProviderAsync_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _repository.RemoveNetworkTypeAsync(999);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Registro no encontrado.", result.Message);
        }

    }
}

    
