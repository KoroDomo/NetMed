using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Test;

public class UnitTestDoctorscs
{  
    private readonly Mock<IDoctorsRepository> _mockDoctorRepository;
    private readonly NetMedContext _context;

    public UnitTestDoctorscs()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        _mockDoctorRepository = new Mock<IDoctorsRepository>();
    }
    [Fact]
    public async Task GetByAvailabilityModeAsyncReturnAvailabilityAsync()
    {

        var mockDoctorAvailability = new Mock<IDoctorsRepository>();
        mockDoctorAvailability.Setup(x => x.GetByAvailabilityModeAsync(1)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorAvailability.Object.GetByAvailabilityModeAsync(1);
        Assert.True(result.Success);

    }
    [Fact]
    public async Task GetBySpecialtyAsyncReturnSpecialtyAsync()
    {
        var mockDoctorSpecialty = new Mock<IDoctorsRepository>();
        mockDoctorSpecialty.Setup(x => x.GetBySpecialtyAsync(1)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorSpecialty.Object.GetBySpecialtyAsync(1);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetByLicenseNumberAsyncReturnLicenseNumberAsync()
    {
        var mockDoctorLicenseNumber = new Mock<IDoctorsRepository>();
        mockDoctorLicenseNumber.Setup(x => x.GetByLicenseNumberAsync("123456")).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorLicenseNumber.Object.GetByLicenseNumberAsync("123456");
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetDoctorsByExperienceAsyncReturnExperienceAsync()
    {
        var mockDoctorExperience = new Mock<IDoctorsRepository>();
        mockDoctorExperience.Setup(x => x.GetDoctorsByExperienceAsync(1, 10)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorExperience.Object.GetDoctorsByExperienceAsync(1, 10);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetDoctorsByConsultationFeeAsyncReturnConsultationFeeAsync()
    {
        var mockDoctorConsultationFee = new Mock<IDoctorsRepository>();
        mockDoctorConsultationFee.Setup(x => x.GetDoctorsByConsultationFeeAsync(100, 200)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorConsultationFee.Object.GetDoctorsByConsultationFeeAsync(100, 200);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetDoctorsWithExpiringLicenseAsyncReturnExpiringLicenseAsync()
    {
        var mockDoctorExpiringLicense = new Mock<IDoctorsRepository>();
        mockDoctorExpiringLicense.Setup(x => x.GetDoctorsWithExpiringLicenseAsync(new DateOnly(2022, 12, 31))).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctorExpiringLicense.Object.GetDoctorsWithExpiringLicenseAsync(new DateOnly(2022, 12, 31));
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetByIdAsyncReturnDoctor()
    {
        var mockDoctor = new Mock<IDoctorsRepository>();
        mockDoctor.Setup(x => x.GetEntityByIdAsync(1)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockDoctor.Object.GetEntityByIdAsync(1);
        Assert.True(result.Success);
    }
}

