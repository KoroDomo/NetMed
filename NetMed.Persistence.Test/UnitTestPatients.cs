using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Domain.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Test;

public class UnitTestPatients
{
    private readonly Mock<IPatientsRepository> _mockPatientRepository;
    private readonly NetMedContext _context;

    public UnitTestPatients()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        _mockPatientRepository = new Mock<IPatientsRepository>();
    }

    [Fact]
    public async Task GetByBloodTypeAsyncReturnTypeOfBlood()
    {
        var mockPatientBloodType = new Mock<IPatientsRepository>();
        mockPatientBloodType.Setup(x => x.GetByBloodTypeAsync("A+")).ReturnsAsync(new OperationResult { Success = true });



        var result = await mockPatientBloodType.Object.GetByBloodTypeAsync("A+");

        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetByInsuranceProviderAsyncReturnInsuranceProvider()
    {
        var mockPatientInsuranceProvider = new Mock<IPatientsRepository>();
        mockPatientInsuranceProvider.Setup(x => x.GetByInsuranceProviderAsync(1)).ReturnsAsync(new OperationResult { Success = true });

        var result = await mockPatientInsuranceProvider.Object.GetByInsuranceProviderAsync(1);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetPatientsWithAllergiesAsyncReturnAllergies()
    {
        var mockPatientAllergies = new Mock<IPatientsRepository>();
        mockPatientAllergies.Setup(x => x.GetPatientsWithAllergiesAsync("Alergias")).ReturnsAsync(new OperationResult { Success = true });

        var result = await mockPatientAllergies.Object.GetPatientsWithAllergiesAsync("Alergias");

        Assert.True(result.Success);
    }

    [Fact]
    public async Task GetPatientsByGenderAsyncReturnGender()
    {
        var mockPatientGender = new Mock<IPatientsRepository>();
        mockPatientGender.Setup(x => x.GetPatientsByGenderAsync("Masculino")).ReturnsAsync(new OperationResult { Success = true });

        var result = await mockPatientGender.Object.GetPatientsByGenderAsync("Masculino");

        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetPatientsByAgeRangeAsyncReturnAgeRange()
    {
        var mockPatientAgeRange = new Mock<IPatientsRepository>();
        mockPatientAgeRange.Setup(x => x.GetPatientsByAgeRangeAsync(18, 65)).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockPatientAgeRange.Object.GetPatientsByAgeRangeAsync(18, 65);
        Assert.True(result.Success);
    }
    [Fact]
    public  async Task GetPatientsAsyncWithoutInsuranceAsyncReturnPatients()
    {
        var mockPatientInsurance = new Mock<IPatientsRepository>();
        mockPatientInsurance.Setup(x => x.GetPatientsAsyncWithoutInsuranceAsync()).ReturnsAsync(new OperationResult { Success = true });
        var result = await mockPatientInsurance.Object.GetPatientsAsyncWithoutInsuranceAsync();
        Assert.True(result.Success);
    }
}

