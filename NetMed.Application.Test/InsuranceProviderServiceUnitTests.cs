using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Application.Services;
using NetMed.Domain.Entities;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;
using NetMed.Infraestructure.Validators.Interfaces;
using Xunit;
using NetMed.Infraestructure.Validators.Implementations;
using NetMed.Infraestructure.Logger;

namespace NetMed.Application.Test
{
    public class InsuranceProviderServiceTests
    {
        private readonly Mock<IInsuranceProviderRepository> _mockRepo;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly IMapper _mapper;
        private readonly InsuranceProviderService _service;
        private readonly IInsuranceProviderValidator _validator;

        public InsuranceProviderServiceTests()
        {
            _mockRepo = new Mock<IInsuranceProviderRepository>();
            _mockLogger = new Mock<ICustomLogger>();
            _validator = new InsuranceProviderValidator();

            // Configurar AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SaveInsuranceProviderDto, InsuranceProviders>();
                cfg.CreateMap<UpdateInsuranceProviderDto, InsuranceProviders>();
                cfg.CreateMap<InsuranceProviders, InsuranceProviderDto>();
            });
            _mapper = config.CreateMapper();

            _service = new InsuranceProviderService(
                _mockRepo.Object,
                _mockLogger.Object,
                _mapper
            );
        }

        [Fact]
        public async Task GetAll_ReturnsSuccessWithData()
        {
            // Arrange
            var providers = new List<InsuranceProviders> { new InsuranceProviders { Id = 1, Name = "Test" } };
            var repoResult = new OperationResult { Success = true, Result = providers };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(repoResult);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.IsType<List<InsuranceProviderDto>>(result.Result);
            Assert.Single((List<InsuranceProviderDto>)result.Result);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsProvider()
        {
            // Arrange
            var provider = new InsuranceProviders { Id = 1, Name = "Test" };
            var repoResult = new OperationResult { Success = true, Result = new List<InsuranceProviders> { provider } };

            _mockRepo.Setup(r => r.GetInsurenProviderById(1)).ReturnsAsync(repoResult);

            // Act
            var result = await _service.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Test", ((List<InsuranceProviderDto>)result.Result)[0].Name);
        }

        [Fact]
        public async Task Save_ValidDto_ReturnsSuccess()
        {
            // Arrange
            var dto = new SaveInsuranceProviderDto { Name = "New Provider" };
            var repoResult = new OperationResult { Success = true };

            _mockRepo.Setup(r => r.SaveEntityAsync(It.IsAny<InsuranceProviders>()))
                    .ReturnsAsync(repoResult);

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
            _mockRepo.Verify(r => r.SaveEntityAsync(It.IsAny<InsuranceProviders>()), Times.Once);
        }

        [Fact]
        public async Task Update_ValidDtoWithId_UpdatesCorrectEntity()
        {
            // Arrange
            var dto = new UpdateInsuranceProviderDto
            {
                InsuranceProviderID = 1,  // ID explícito
                Name = "Updated Provider"
            };

            InsuranceProviders capturedEntity = null;

            _mockRepo.Setup(r => r.UpdateEntityAsync(It.IsAny<InsuranceProviders>()))
                    .Callback<InsuranceProviders>(e => capturedEntity = e)
                    .ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
            //Assert.Equal(dto.InsuranceProviderID, capturedEntity.Id); // Verificación del ID
            Assert.Equal(dto.Name, capturedEntity.Name);
        }

        [Fact]
        public async Task Update_NonExistentId_ReturnsNotFoundError()
        {
            // Arrange
            var dto = new UpdateInsuranceProviderDto { InsuranceProviderID = 999 };
            var errorResult = new OperationResult
            {
                Success = false,
                Message = "Registro no encontrado."
            };

            _mockRepo.Setup(r => r.UpdateEntityAsync(It.IsAny<InsuranceProviders>()))
                    .ReturnsAsync(errorResult);

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("no encontrado", result.Message.ToLower());
        }

        //[Fact]
        //public async Task Remove_ValidId_DeactivatesProvider()
        //{
        //    // Arrange
        //    var dto = new RemoveInsuranceProviderDto { InsuranceProviderID = 1 };
        //    var repoResult = new OperationResult { Success = true };

        //    _mockRepo.Setup(r => r.RemoveInsuranceProviderAsync(dto))
        //            .ReturnsAsync(repoResult);

        //    // Act
        //    var result = await _service.Remove(dto);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.True(dto.Removed);
        //}

        [Fact]
        public async Task Remove_NullDto_ReturnsValidationError()
        {
            // Arrange
            RemoveInsuranceProviderDto dto = null;

            // Act
            var result = await _service.Remove(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error al remover el Provider.", result.Message);
        }

        [Fact]
        public async Task GetAll_RepositoryFails_ReturnsError()
        {
            // Arrange
            var repoResult = new OperationResult { Success = false };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(repoResult);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Save_DatabaseError_LogsError()
        {
            // Arrange
            _mockRepo.Setup(r => r.SaveEntityAsync(It.IsAny<InsuranceProviders>()))
                    .ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _service.Save(new SaveInsuranceProviderDto());

            // Assert
            Assert.False(result.Success);
            _mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }
}