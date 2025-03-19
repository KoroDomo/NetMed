using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Model.Models;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NPOI.SS.Formula.Functions;

namespace NetMed.Persistence.Test
{
    public class InsuranceProviderRepositoryUnitTests
    {
        private readonly NetMedContext _context;
        private readonly Mock<ICustomLogger> _mockLogger;
        private readonly IInsuranceProviderRepository _repository;

        public InsuranceProviderRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new NetMedContext(options);

            _mockLogger = new Mock<ICustomLogger>();
            _repository = new InsuranceProviderRepository(_context, _mockLogger.Object);
        }

        [Fact] //SaveEntityAsync
        public async Task SaveEntityAsync_ValidProvider_ReturnsSuccess()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Id = 5,
                Name = "Provider 2222",
                ContactNumber = "8071111111",
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
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548

            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("Registro guardado exitosamente.", result.Message);
            
        }

        [Fact]
        public async Task SaveEntityAsync_DuplicateName_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvider",
                ContactNumber = "8091111111",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                CustomerSupportContact = "8091111121",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
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
            Assert.Contains("Ya existe un InsuranceProvider con este nombre.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidPhoneNumber_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111dfbhsdfbhsdfhb111",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El numero de telefono no es valido.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidEmail_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvi2der",
                ContactNumber = "8091111111",
                Email = "provider2",
                Website = "www.provider2.com",
                CustomerSupportContact = "8091111121",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El correo electronico no es valido.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidWebsite_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingPro3vider",
                ContactNumber = "8091111111",
                Email = "provider2@gmail.com",
                Website = "www.provider2.comasdddddwqewqewqe" +
                "wqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqe" +
                "wqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqewqe" +
                "wqewqewqewqewqewqewqewqewqewqewqewqeerrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr" +
                "rrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrd",
                CustomerSupportContact = "8091111121",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo Website excede la longitud maxima permitida.", result.Message);
        }
        [Fact]
        public async Task SaveEntityAsync_InvalidCustomerSupportContact_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El numero de telefono no es valido.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidCity_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "Citwerrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr" +
                "rsdfffffffrrrrrrrrrrrrrrrrrhghghghghghghghghghgjrrrrry",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };
            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo City excede la longitud maxima permitida.", result.Message);
        }
        [Fact]
        public async Task SaveEntityAsync_InvalidState_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "City",
                State = "Stasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddate",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };
            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo State excede la longitud maxima permitida.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidCountry_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "City",
                State = "State",
                Country = "Stasddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddate",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };
            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo Country excede la longitud maxima permitida.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidZipCode_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "City",
                State = "State",
                Country = "Contry",
                ZipCode = "0414141404145104145145141",
                CoverageDetails = "Details",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };
            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo ZipCode excede la longitud maxima permitida.", result.Message);
        }

        [Fact]
        public async Task SaveEntityAsync_InvalidMaxCoverageAmount_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Name = "ExistingProvid1er",
                ContactNumber = "8091111121",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                CustomerSupportContact = "8091111121",
                City = "City",
                State = "State",
                Country = "Contry",
                ZipCode = "12345",
                CoverageDetails = "null",
                IsPreferred = false,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 0
            };
            // Act
            var result = await _repository.SaveEntityAsync(provider);
            await _context.SaveChangesAsync();

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(await _repository.GetInsurenProviderById(provider.Id));
            Assert.Contains("El campo MaxCoverageAmount no es valido.", result.Message);
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
                ContactNumber = "809-820-6576",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
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
            Assert.Contains("Provider/s obtenido exitosamente.", result.Message);

        }

        [Fact]
        public async Task GetInsurenProviderById_ShouldReturnNotFound_WhenProviderDoesNotExist()
        {
            // Arrange
            int insuranceId = 999;
            var provider = new InsuranceProviders
            {
                Name = "Provider 2222",
                ContactNumber = "8071111111",
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
                IsPreferred = false,
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
            Assert.Equal("Registro no encontrado.", result.Message);
        }

        [Fact] //GettAllAsync
        public async Task GetAllAsync_ReturnsProviders()
        {
            // Arrange
            var provider1 = new InsuranceProviders
            {
                Name = "ExistingProvider1",
                ContactNumber = "809-111-1111",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider1);

            var provider2 = new InsuranceProviders
            {
                Name = "ExistingProvider2",
                ContactNumber = "809-111-1112",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
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
            Assert.Contains("Provider/s obtenido exitosamente.", result.Message);
        }

        [Fact]//UpdateEntityAsync
        public async Task UpdateEntityAsync_ValidProvider_ReturnsSuccess()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Id = 10,
                Name = "Provider3",
                ContactNumber = "8071111111",
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
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548
            };
            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            
            provider.Name = "Provider Actualizado";

            // Act
            var result = await _repository.UpdateEntityAsync(provider);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Provider Actualizado", ((InsuranceProviders)result.Result).Name);
            Assert.Contains("Registro actualizado exitosamente.", result.Message);
        }

        [Fact]
        public async Task UpdateEntityAsync_NonExistentProvider_ReturnsError()
        {
            // Arrange
            var provider = new InsuranceProviders {
                Id = 984,
                Name = "Provider3",
                ContactNumber = "8071111111",
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
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548
            };

            // Act
            var result = await _repository.UpdateEntityAsync(provider);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Registro no encontrado", result.Message);
        }

        [Fact] //RemoveInsuranceProviderAsync
        public async Task RemoveInsuranceProviderAsync_ValidId_DeactivatesProvider()
        {
            // Arrange
            var provider = new InsuranceProviders
            {
                Id = 7,
                Name = "Provideeeer",
                ContactNumber = "8071111111",
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
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 14548
            };

            _context.InsuranceProviders.Add(provider);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.RemoveInsuranceProviderAsync(7);

            // Assert
            Assert.True(result.Success);
            Assert.False(provider.IsActive);
            Assert.Contains("Provider eliminado exitosamente.", result.Message);
        }

        [Fact]
        public async Task RemoveInsuranceProviderAsync_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _repository.RemoveInsuranceProviderAsync(999);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Registro no encontrado.", result.Message);
        }

        [Fact] //GetPreferredInsuranceProvidersAsync
        public async Task GetPreferredInsuranceProvidersAsync_ReturnsPreferred()
        {
            // Arrange
            _context.InsuranceProviders.AddRange(
                new InsuranceProviders {
                    Id = 84,
                    Name = "ExistingPr",
                    ContactNumber = "809-111-1111",
                    Email = "provider1@gmail.com",
                    Website = "www.provider1.com",
                    Address = "123 Main St",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    ZipCode = "12345",
                    LogoUrl = "assa",
                    CoverageDetails = "Details",
                    IsPreferred = true,
                    IsActive = true,
                    NetworkTypeID = 1,
                    AcceptedRegions = "Region",
                    MaxCoverageAmount = 100
                },
                new InsuranceProviders {
                    Id = 94,
                    Name = "ExistingPr2",
                    ContactNumber = "809-111-1111",
                    Email = "provider1@gmail.com",
                    Website = "www.provider1.com",
                    Address = "123 Main St",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    ZipCode = "12345",
                    LogoUrl = "assa",
                    CoverageDetails = "Details",
                    IsPreferred = false,
                    IsActive = true,
                    NetworkTypeID = 1,
                    AcceptedRegions = "Region",
                    MaxCoverageAmount = 100
                }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPreferredInsuranceProvidersAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Single((List<InsuranceProviderModel>)result.Result);
            Assert.Contains("Provider/s con preferencia obtenidos exitosamente.", result.Message);
        }

        [Fact] //GetActiveInsuranceProvidersAsync
        public async Task GetActiveInsuranceProvidersAsync_ReturnsActive()
        {
            // Arrange
            var provider1 = new InsuranceProviders
            {
                Name = "ExistingProvider1",
                ContactNumber = "809-111-1111",
                Email = "provider1@gmail.com",
                Website = "www.provider1.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
                IsActive = true,
                NetworkTypeID = 1,
                AcceptedRegions = "Region",
                MaxCoverageAmount = 100
            };

            _context.InsuranceProviders.Add(provider1);

            var provider2 = new InsuranceProviders
            {
                Name = "ExistingProvider2",
                ContactNumber = "809-111-1112",
                Email = "provider2@gmail.com",
                Website = "www.provider2.com",
                Address = "123 Main St",
                City = "City",
                State = "State",
                Country = "Country",
                ZipCode = "12345",
                CoverageDetails = "Details",
                IsPreferred = false,
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
            Assert.Contains("Provider/s activos obtenidos exitosamente.", result.Message);
        }
    }
}
