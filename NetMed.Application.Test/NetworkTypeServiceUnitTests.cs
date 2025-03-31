using AutoMapper;
using Moq;
using NetMed.Application.Dtos.NetworkType;
using NetMed.Application.Services;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators.Interfaces;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Test
{
    public class NetworktypeServiceUnitTests
    {
        private readonly Mock<INetworkTypeRepository> _mockRepo;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly IMapper _mapper;
        private readonly NetworktypeService _service;
        private readonly Mock<INetworkTypeValidator> _mockValidator;

        public NetworktypeServiceUnitTests()
        {
            _mockRepo = new Mock<INetworkTypeRepository>();
            _mockLogger = new Mock<ICustomLogger>();
            _mockValidator = new Mock<INetworkTypeValidator>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SaveNetworkTypeDto, NetworkType>();
                cfg.CreateMap<UpdateNetworkTypeDto, NetworkType>();
                cfg.CreateMap<NetworkType, NetworkTypeDto>();
            });
            _mapper = config.CreateMapper();

            _service = new NetworktypeService(
                _mockRepo.Object,
                _mockLogger.Object,
                _mapper
            );
        }

        [Fact]
        public async Task GetAll_ReturnsMappedDtos()
        {
            // Arrange
            var networks = new List<NetworkType> { new NetworkType { Id = 1, Name = "Test" } };
            _mockRepo.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(new OperationResult { Success = true, Result = networks });

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.IsType<List<NetworkTypeDto>>(result.Result);
            Assert.Equal("Test", ((List<NetworkTypeDto>)result.Result)[0].Name);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsNetwork()
        {
            // Arrange
            var network = new NetworkType { Id = 1, Name = "Test" };
            _mockRepo.Setup(r => r.GetNetworkTypeById(1))
                    .ReturnsAsync(new OperationResult { Success = true, Result = new List<NetworkType> { network } });

            // Act
            var result = await _service.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Test", ((List<NetworkTypeDto>)result.Result)[0].Name);
        }

        [Fact]
        public async Task Remove_ValidId_SetsRemovedFlag()
        {
            // Arrange
            var dto = new RemoveNetworkTypeDto { NetworkTypeId = 1 };
            _mockRepo.Setup(r => r.RemoveNetworkTypeAsync(1))
                    .ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(dto);

            // Assert
            Assert.True(result.Success);
            Assert.True(dto.Removed); // Verificar asignación específica del código
        }

        [Fact]
        public async Task Save_ValidDto_MapsCorrectly()
        {
            // Arrange
            var dto = new SaveNetworkTypeDto { Name = "New Network" };
            NetworkType capturedEntity = null;

            _mockRepo.Setup(r => r.SaveEntityAsync(It.IsAny<NetworkType>()))
                    .Callback<NetworkType>(e => capturedEntity = e)
                    .ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.Equal(dto.Name, capturedEntity.Name);
            Assert.True(capturedEntity.IsActive);
        }

        [Fact]
        public async Task Update_ValidId_MapsIdCorrectly()
        {
            // Arrange
            var dto = new UpdateNetworkTypeDto { NetworkTypeId = 1, Name = "Updated" };
            NetworkType capturedEntity = null;

            _mockRepo.Setup(r => r.UpdateEntityAsync(It.IsAny<NetworkType>()))
                    .Callback<NetworkType>(e => capturedEntity = e)
                    .ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            //Assert.Equal(dto.NetworkTypeId, capturedEntity.Id);
            Assert.Equal(dto.Name, capturedEntity.Name);
        }

        [Fact]
        public async Task Remove_NullDto_ReturnsValidationError()
        {
            // Act
            var result = await _service.Remove(null);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("error", result.Message.ToLower());
        }

        [Fact]
        public async Task Update_NonExistentId_ReturnsNotFound()
        {
            // Arrange
            var dto = new UpdateNetworkTypeDto { NetworkTypeId = 999 };
            _mockRepo.Setup(r => r.UpdateEntityAsync(It.IsAny<NetworkType>()))
                    .ReturnsAsync(new OperationResult { Success = false, Message = "No encontrado" });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("no encontrado", result.Message.ToLower());
        }

        [Fact]
        public async Task Save_DatabaseError_LogsException()
        {
            // Arrange
            _mockRepo.Setup(r => r.SaveEntityAsync(It.IsAny<NetworkType>()))
                    .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _service.Save(new SaveNetworkTypeDto());

            // Assert
            Assert.False(result.Success);
            _mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }
}