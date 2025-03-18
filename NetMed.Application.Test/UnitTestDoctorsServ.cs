

using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;
namespace NetMed.Application.Test
{
    public class UnitTestDoctorsServ
    {
        private readonly Mock<IDoctorsServices> _mockDoctorsServices;
        private readonly Mock<DoctorsRepository> _mockRepository;
        private readonly NetMedContext _context;

        public UnitTestDoctorsServ()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
           .UseInMemoryDatabase(databaseName: "MedicalAppointment")
           .Options;
            _context = new NetMedContext(options);
            _mockDoctorsServices = new Mock<IDoctorsServices>();

            _mockRepository = new Mock<DoctorsRepository>(_context);
        }
        [Fact]

        public async Task GetAllDataReturnAllDoctorsData()
        {
            // Arrange
            var mockData = new OperationResult
            {
                Success = true,
                data = new List<Doctors> { new Doctors { UserId = 6, SpecialtyID = 3, LicenseNumber = "133333", PhoneNumber = "809333892", YearsOfExperience = 10, Education = "Medical School", Bio = "Experienced doctor", ConsultationFee = 1000, ClinicAddress = "147 Clinton Av", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), AvailabilityModeId = 1 } }
            };
            _mockDoctorsServices.Setup(x => x.GetAllData()).ReturnsAsync(mockData);

            // Act
            var result = await _mockDoctorsServices.Object.GetAllData();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.data);
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
            _mockDoctorsServices.Setup(x => x.GetById(1)).ReturnsAsync(mockData);
            // Act
            var result = await _mockDoctorsServices.Object.GetById(1);
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
                data = new Doctors { UserId = 9, SpecialtyID = 2, LicenseNumber = "59343", PhoneNumber = "849222013", YearsOfExperience = 2, Education = "UCE", Bio = "Recommended Doctor", ConsultationFee = 400, ClinicAddress = "Santo Domingo", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) , AvailabilityModeId = 1 }
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

            _mockDoctorsServices.Setup(x => x.Add(It.IsAny<AddDoctorsDto>())).ReturnsAsync(mockData);

            // Act
            var result = await _mockDoctorsServices.Object.Add(addDoctorsDto);

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
                data = new Doctors { UserId = 9, SpecialtyID = 2, LicenseNumber = "59343", PhoneNumber = "849222013", YearsOfExperience = 2, Education = "UCE", Bio = "Recommended Doctor", ConsultationFee = 400, ClinicAddress = "Santo Domingo", LicenseExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)) , AvailabilityModeId = 1 }
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

            _mockDoctorsServices.Setup(x => x.Update(It.IsAny<UpdateDoctorsDto>())).ReturnsAsync(mockData);

            // Act
            var result = await _mockDoctorsServices.Object.Update(updateDoctorsDto);

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

            _mockDoctorsServices.Setup(x => x.Delete(It.IsAny<DeleteDoctorDto>())).ReturnsAsync(mockData);

            // Act
            var result = await _mockDoctorsServices.Object.Delete(deleteDoctorDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

       
    }
    
}
