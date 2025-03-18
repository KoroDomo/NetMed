using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Persistence.Test;

public class UnitTestPatients
{
    private readonly PatientsRepository _patientsRepository;
    private readonly NetMedContext _context;
    private readonly RepErrorMapper _repErrorMapper;

    public UnitTestPatients()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        var logger = new Mock<ILogger<PatientsRepository>>().Object;
        _repErrorMapper = new RepErrorMapper(); // Initialize _repErrorMapper before using it
        _patientsRepository = new PatientsRepository(_context, logger, _repErrorMapper);
    }
    [Fact]
    public async Task GetByBloodTypeAsyncReturnTypeOfBlood()
    {
        // Create a test patient
        var patient = new Patients
        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890",
        };

        // Add the patient to your context
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        // Now test
        var result = await _patientsRepository.GetByBloodTypeAsync('O');
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Tipo de sangre encontrado", result.Message);

    }

    [Fact]
    public async Task GetByInsuranceProviderAsyncReturnInsuranceProvider()
    {
        var patient = new Patients
        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890",
            InsuranceProviderID = 304
        };

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        var result = await _patientsRepository.GetByInsuranceProviderAsync(304);
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Proveedor de seguro encontrado", result.Message);
    }

    [Fact]
    public async Task GetPatientsWithAllergiesAsyncReturnAllergies()
    {

        var patient = new Patients
        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890"
        };

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        var result = await _patientsRepository.GetPatientsWithAllergiesAsync("none");
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetPatientsByGenderAsyncReturnGender()
    {
        var patients = new Patients

        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890",
            Gender = 'M'

        };
        await _context.Patients.AddAsync(patients);
        await _context.SaveChangesAsync();

        var result = await _patientsRepository.GetPatientsByGenderAsync('M');

        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]

    public async Task GetPatientsAddressAsyncReturnAddress()
    {
        var patients = new Patients
        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890",
            Address = "123 Main St"
        };
        await _context.Patients.AddAsync(patients);
        await _context.SaveChangesAsync();
        var result = await _patientsRepository.SearchByAddressAsync("123 Main St");
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetEmergencyContactAsyncReturnEmergencyContact()
    {
        var patients = new Patients
        {
            UserId = 1,
            BloodType = 'O',
            DateOfBirth = new DateOnly(1990, 12, 31),
            EmergencyContactPhone = "123-456-7890",
            EmergencyContactName = "Juan Perez",
            Allergies = "none",
            PhoneNumber = "123-456-7890",
            Address = "123 Main St"
        };
        await _context.Patients.AddAsync(patients);
        await _context.SaveChangesAsync();
        var result = await _patientsRepository.GetByEmergencyContactAsync("Juan Perez");
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
}

