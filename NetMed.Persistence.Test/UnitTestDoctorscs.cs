using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;

namespace NetMed.Persistence.Test;

public class UnitTestDoctorscs
{  
   
    private readonly NetMedContext _context;
    private readonly DoctorsRepository _doctorsRepository;
    private readonly RepErrorMapper repErrorMapper;


    public UnitTestDoctorscs()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
           .UseInMemoryDatabase(databaseName: "Quegrasa")
           .Options;
        _context = new NetMedContext(options);
        var logger = new Mock<ILogger<DoctorsRepository>>().Object;
        repErrorMapper = new RepErrorMapper();
        _doctorsRepository = new DoctorsRepository(_context, logger, repErrorMapper); 
    }
   
    
    [Fact]
    public async Task GetByAvailabilityModeAsyncReturnAvailabilityAsync()
    {
        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act

        var result = await _doctorsRepository.GetByAvailabilityModeAsync(1);

        //assert
        Assert.True(result.Success);
        Assert.Equal("Doctor disponible", result.Message);

    }
    [Fact]
    public async Task GetBySpecialtyAsyncReturnSpecialtyAsync()
    {
        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act

        var result = await _doctorsRepository.GetBySpecialtyAsync(1);
        Assert.True(result.Success);
        Assert.Equal("Doctor especializado encontrado", result.Message);
        Assert.Equal(1, doctor.SpecialtyID);
    }
    [Fact]
    public async Task GetByLicenseNumberAsyncReturnLicenseNumberAsync()
    {

        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act
        var result = await _doctorsRepository.GetByLicenseNumberAsync("123456");
        Assert.True(result.Success);
        Assert.Equal("Numero de licencia encontrado", result.Message);
        Assert.Equal("123456", doctor.LicenseNumber);
    }
    [Fact]
    public async Task GetDoctorsByExperienceAsyncReturnExperienceAsync()
    {
        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act
        var result = await _doctorsRepository.GetDoctorsByExperienceAsync(5);
        Assert.True(result.Success);
        Assert.Equal("Doctor con experiencia", result.Message);
        Assert.False(result.Success == false);
    }
    [Fact]
    public async Task GetDoctorsByConsultationFeeAsyncReturnConsultationFeeAsync()
    {
      var doctor = new Doctors
      {
          UserId = 1,
          SpecialtyID = 1,
          LicenseNumber = "123456",
          YearsOfExperience = 5,
          ConsultationFee = 100,
          AvailabilityModeId = 1,
          IsActive = true,
          LicenseExpirationDate = new DateOnly(2022, 12, 31),
          PhoneNumber = "555-1234",
          Education = "MD",
          ClinicAddress = "123 Main St"
      };
        //act
        var result = await _doctorsRepository.GetDoctorsByConsultationFeeAsync(100);

        Assert.True(result.Success);
        Assert.Equal("Doctor con tarifa de consulta", result.Message);
        Assert.Equal(100, doctor.ConsultationFee);
    }
    [Fact]
    public async Task GetDoctorsWithExpiringLicenseAsyncReturnExpiringLicenseAsync()
    {

        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act
        var result = await _doctorsRepository.GetDoctorsWithExpiringLicenseAsync(new DateOnly(2022, 12, 31));
        Assert.True(result.Success);
        Assert.Equal("Doctor con licencia expirada", result.Message);
        Assert.Equal(new DateOnly(2022, 12, 31), doctor.LicenseExpirationDate);
    }
    [Fact]
    public async Task GetByIdAsyncReturnDoctor()
    {
        
        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        //act
        var result = await _doctorsRepository.GetEntityByIdAsync(1);
        Assert.True(result.Success, $"Failed with message: {result.Message}");
        Assert.Equal("Doctor encontrado", result.Message);
        Assert.Equal(1, doctor.UserId);
    }

    [Fact]

    public async Task GetActiveDoctorsAsyncReturnActiveDoctors()
    {

        var doctor = new Doctors
        {
            UserId = 1,
            SpecialtyID = 1,
            LicenseNumber = "123456",
            YearsOfExperience = 5,
            ConsultationFee = 100,
            AvailabilityModeId = 1,
            IsActive = true,
            LicenseExpirationDate = new DateOnly(2022, 12, 31),
            PhoneNumber = "555-1234",
            Education = "MD",
            ClinicAddress = "123 Main St"
        };
        //act
        var result = await _doctorsRepository.GetActiveDoctorsAsync(true);
        Assert.True(result.Success);
        Assert.Equal("Doctor activo", result.Message);
        
    }
}

