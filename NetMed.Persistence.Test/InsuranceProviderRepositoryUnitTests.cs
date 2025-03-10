using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Model.Models;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Validators;

namespace NetMed.Persistence.Test
{
    public class InsuranceProviderRepositoryUnitTests
    {
        private readonly NetMedContext _context;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly InsuranceProviderValidator _validator;
        private readonly InsuranceProviderRepository _repository;

        public InsuranceProviderRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new NetMedContext(options);

            _mockLogger = new Mock<ICustomLogger>();
            _mockConfiguration = new Mock<IConfiguration>();
            _validator = new InsuranceProviderValidator(_mockConfiguration.Object);
            _repository = new InsuranceProviderRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        [Fact] //SaveEntityAsync
        public async Task SaveEntityAsync_ValidProvider_ReturnsSuccess()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Id = 1,
                Name = "Provider 2222",
                PhoneNumber = "8071111111",
                Email = "provder2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Man St",
                City = "City",
                State = "Stat",
                LogoUrl = "gffdg",
                CustomerSupportContact= "8091111121",
                Country = "Counry",
                ZipCode = "1245",
                CoverageDetails = "Dtails",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548

            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            
            // Assert

            Assert.True(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            
        }

        [Fact]
        public async Task SaveEntityAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvider",
                PhoneNumber = "8091111111",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            var newProvider = new InsuranceProviders { Name = "ExistingProvider" };

            // Act
            var result = await _repository.SaveEntityAsync(newProvider);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ya existe un InsuranceProvider con este nombre", result.Message);
        }

        [Fact] //GetInsurenProviderById
        public async Task GetInsurenProviderById_ShouldReturnProvider_WhenProviderExists()
        {
            // Arrange
            int insuranceId = 15;
            var provider = new InsuranceProviders
            {
                Id = insuranceId,
                Name = "Provider 1",
                PhoneNumber = "809-820-6576",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            
            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetInsurenProviderById(insuranceId);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal(insuranceId, result.Result[0].Id);
            Assert.Equal("Provider 1", result.Result[0].Name);

        }

        [Fact]
        public async Task GetInsurenProviderById_ShouldReturnNotFound_WhenProviderDoesNotExist()
        {
            // Arrange
            int insuranceId = 999;
            var provider = new InsuranceProviders
            {
                Name = "Provider 2222",
                PhoneNumber = "8071111111",
                Email = "provder2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Man St",
                City = "City",
                State = "Stat",
                LogoUrl = "gffdg",
                CustomerSupportContact = "8091111121",
                Country = "Counry",
                ZipCode = "1245",
                CoverageDetails = "Dtails",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548
            };

            // Act
            var result = await _repository.GetInsurenProviderById(insuranceId);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.Equal("No se encontró el insuranceProvider con el ID: 999.", result.Message);
        }

        [Fact] //GettAllAsync
        public async Task GetAllAsync_ReturnsProviders()
        {
            // Arrange
            var provider1 = new InsuranceProviders
            {
                Name = "ExistingProvider1",
                PhoneNumber = "809-111-1111",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider1);

            var provider2 = new InsuranceProviders
            {
                Name = "ExistingProvider2",
                PhoneNumber = "809-111-1112",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result);
            //Assert.Contains("Proveedores de seguros obtenidos exitosamente.", result.Message);
        }

        [Fact] //UpdateEntityAsync
        public async Task UpdateEntityAsync_ValidUpdate_ReturnsSuccess()
        {
            // Arrange
            var provider = new InsuranceProviders { Id = 1, Name = "OldName" };
            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            provider.Name = "NewName";

            // Act
            var result = await _repository.UpdateEntityAsync(provider);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("NewName", _context.InsuranceProviders.Find(1).Name);
        }

        [Fact]
        public async Task UpdateEntityAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            _context.InsuranceProviders.AddRange(
                new InsuranceProviders { Id = 1, Name = "Provider1" },
                new InsuranceProviders { Id = 2, Name = "Provider2" }
            );
            await _context.SaveChangesAsync();

            var providerToUpdate = _context.InsuranceProviders.Find(2);
            providerToUpdate.Name = "Provider1"; // Nombre duplicado


            // Act
            var result = await _repository.UpdateEntityAsync(providerToUpdate);

            // Assert
            Assert.False(result.Success);
        }

        [Fact] //RemoveInsuranceProviderAsync
        public async Task RemoveInsuranceProviderAsync_ValidId_DeactivatesProvider()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Id = 1,
                Name = "ExistingProvider12",
                PhoneNumber = "809-111-1111",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.RemoveInsuranceProviderAsync(1);

            // Assert
            Assert.Contains("Proveedores de removido exitosamente.", result.Message);
            Assert.True(result.Success);
            Assert.False(_context.InsuranceProviders.Find(1).IsActive);
        }

        [Fact]
        public async Task RemoveInsuranceProviderAsync_InvalidId_ReturnsError()
        {
            // Arrange

            // Act
            var result = await _repository.RemoveInsuranceProviderAsync(999);

            // Assert
            Assert.False(result.Success);
        }

        [Fact] //GetPreferredInsuranceProvidersAsync
        public async Task GetPreferredInsuranceProvidersAsync_ReturnsPreferred()
        {
            // Arrange
            _context.InsuranceProviders.AddRange(
                new InsuranceProviders { IsPreferred = true },
                new InsuranceProviders { IsPreferred = false }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPreferredInsuranceProvidersAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Single((List<InsuranceProviderModel>)result.Result);
        }

        [Fact] //GetActiveInsuranceProvidersAsync
        public async Task GetActiveInsuranceProvidersAsync_ReturnsActive()
        {
            // Arrange
            var provider1 = new InsuranceProviders
            {
                Name = "ExistingProvider1",
                PhoneNumber = "809-111-1111",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider1);

            var provider2 = new InsuranceProviders
            {
                Name = "ExistingProvider2",
                PhoneNumber = "809-111-1112",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = true,
                IsActive = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };
            _context.InsuranceProviders.Add(provider2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetActiveInsuranceProvidersAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Single((List<InsuranceProviderModel>)result.Result);
        }
    }
}
