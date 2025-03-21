

using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Application.Services;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
namespace NetMed.Application.Test
{
    public class UnitTestDoctorsServ
    {
        private readonly Mock<IDoctorsRepository> _mockDoctorsRepositories;
        private readonly ILogger<DoctorsServices> _logger;
        private readonly IDoctorsServices _doctorsServices;

        public UnitTestDoctorsServ()
        {
            _mockDoctorsRepositories = new Mock<IDoctorsRepository>();
            _logger = new Mock<ILogger<DoctorsServices>>().Object;

            _doctorsServices = new DoctorsServices(_mockDoctorsRepositories.Object, _logger);
        }
        [Fact]
        public async Task GetAllDataReturnAllDoctorsData()
        {
            // Arrange
            var doctors = new List<Doctors>
                {
                    new Doctors { UserId = 1, SpecialtyID = 1, LicenseNumber = "12345", PhoneNumber = "1234567890", YearsOfExperience = 5, Education = "Medical School", Bio = "Experienced doctor", ConsultationFee = 100, ClinicAddress = "123 Clinic St", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 },
                    new Doctors { UserId = 2, SpecialtyID = 2, LicenseNumber = "67890", PhoneNumber = "0987654321", YearsOfExperience = 10, Education = "Another Medical School", Bio = "Highly experienced doctor", ConsultationFee = 200, ClinicAddress = "456 Clinic St", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(2)), AvailabilityModeId = 2 }
                };

            var result = new OperationResult
            {
                Success = true,
                data = doctors
            };
            _mockDoctorsRepositories.Setup(x => x.GetAllAsync()).ReturnsAsync(result);
            // Act
            var resultData = await _doctorsServices.GetAllData();
            // Assert
            Assert.NotNull(resultData);
            Assert.NotNull(resultData.data);
            Assert.Equal(2, resultData.data?.Count);
        }

        [Fact]
        public async Task GetDoctorByIdReturnDoctorDataById()
        {
            // Arrange
            var mockData = new OperationResult
            {
                Success = true,
                data = new Doctors { UserId = 1, SpecialtyID = 1, LicenseNumber = "12345", PhoneNumber = "1234567890", YearsOfExperience = 5, Education = "Medical School", Bio = "Experienced doctor", ConsultationFee = 100, ClinicAddress = "123 Clinic St", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 }
            };
            _mockDoctorsRepositories.Setup(x => x.GetEntityByIdAsync(1)).ReturnsAsync(mockData);
            // Act
            var result = await _doctorsServices.GetById(1);
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.data);
        }

        [Fact]
        public async Task AddDoctorReturnDoctorAdded()
        {
            var mockData = new OperationResult
            {
                Success = true,
                data = new Doctors { UserId = 9, SpecialtyID = 2, LicenseNumber = "59343", PhoneNumber = "849222013", YearsOfExperience = 2, Education = "UCE", Bio = "Recommended Doctor", ConsultationFee = 400, ClinicAddress = "Santo Domingo", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 }
            };

            var addDoctorsDto = new AddDoctorsDto
            {
                UserId = mockData.data.UserId,
                SpecialtyID = mockData.data.SpecialtyID,
                LicenseNumber = mockData.data.LicenseNumber,
                PhoneNumber = mockData.data.PhoneNumber,
                YearsOfExperience = mockData.data.YearsOfExperience,
                Education = mockData.data.Education,
                Bio = mockData.data.Bio,
                ConsultationFee = mockData.data.ConsultationFee,
                ClinicAddress = mockData.data.ClinicAddress,
                LicenseExpirationDate = mockData.data.LicenseExpirationDate,
                AvailabilityModeId = mockData.data.AvailabilityModeId
            };
            var result = new OperationResult
            {
                Success = true,
                data = mockData
            };

            _mockDoctorsRepositories.Setup(x => x.SaveEntityAsync(It.IsAny<Doctors>())).ReturnsAsync(mockData);

            // Act
            var resultData = await _doctorsServices.Add(addDoctorsDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.data);
        }
        [Fact]

        public async Task UpdateDoctorReturnUpdatedDoctorData()
        {
            // Arrange
            var mockData = new OperationResult
            {
                Success = true,
                data = new Doctors { UserId = 9, SpecialtyID = 2, LicenseNumber = "59343", PhoneNumber = "849222013", YearsOfExperience = 2, Education = "UCE", Bio = "Recommended Doctor", ConsultationFee = 400, ClinicAddress = "Santo Domingo", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 }
            };

            var updateDoctorsDto = new UpdateDoctorsDto
            {
                UserId = mockData.data.UserId,
                SpecialtyID = mockData.data.SpecialtyID,
                LicenseNumber = mockData.data.LicenseNumber,
                PhoneNumber = mockData.data.PhoneNumber,
                YearsOfExperience = mockData.data.YearsOfExperience,
                Education = mockData.data.Education,
                Bio = mockData.data.Bio,
                ConsultationFee = mockData.data.ConsultationFee,
                ClinicAddress = mockData.data.ClinicAddress,
                LicenseExpirationDate = mockData.data.LicenseExpirationDate,
                AvailabilityModeId = mockData.data.AvailabilityModeId
            };

            _mockDoctorsRepositories.Setup(x => x.UpdateEntityAsync(It.IsAny<Doctors>())).ReturnsAsync(mockData);

            // Act
            var result = await _doctorsServices.Update(updateDoctorsDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.data);
        }

        [Fact]
        public async Task DeleteDoctorReturnSuccess()
        {
            // Arrange
            var mockData = new OperationResult
            {
                Success = true,
                data = new Doctors { UserId = 9, SpecialtyID = 2, LicenseNumber = "59343", PhoneNumber = "849222013", YearsOfExperience = 2, Education = "UCE", Bio = "Recommended Doctor", ConsultationFee = 400, ClinicAddress = "Santo Domingo", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 }
            };

            var deleteDoctorDto = new DeleteDoctorDto
            {
                UserId = mockData.data?.UserId ?? 0,
                SpecialtyID = mockData.data?.SpecialtyID ?? 0,
                LicenseNumber = mockData.data?.LicenseNumber ?? string.Empty,
                PhoneNumber = mockData.data?.PhoneNumber ?? string.Empty,
                YearsOfExperience = mockData.data?.YearsOfExperience ?? 0,
                Education = mockData.data?.Education ?? string.Empty,
                Bio = mockData.data?.Bio ?? string.Empty,
                ConsultationFee = mockData.data?.ConsultationFee ?? 0,
                ClinicAddress = mockData.data?.ClinicAddress ?? string.Empty,
                LicenseExpirationDate = mockData.data?.LicenseExpirationDate ?? DateOnly.MinValue,
                AvailabilityModeId = mockData.data?.AvailabilityModeId ?? 0
            };

            _mockDoctorsRepositories.Setup(x => x.DeleteEntityAsync(It.IsAny<Doctors>())).ReturnsAsync(mockData);

            // Act
            var result = await _doctorsServices.Delete(deleteDoctorDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }


    }
    
}
