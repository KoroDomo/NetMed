using Microsoft.EntityFrameworkCore;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.PatientsDto;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Test;

public class UnitTestPatientsServ
{
    private readonly NetMedContext _context;
    private readonly Mock<IPatientsServices> _mockPatientServices;
    private readonly Mock<PatientsRepository> _mockPatientRepositoy;

    public UnitTestPatientsServ()
    {
        var options = new DbContextOptionsBuilder<NetMedContext>()
            .UseInMemoryDatabase(databaseName: "MedicalAppointment")
            .Options;
        _context = new NetMedContext(options);
        _mockPatientServices = new Mock<IPatientsServices>();
        _mockPatientRepositoy = new Mock<PatientsRepository>();
    }

    [Fact]

    public async Task GetAllDataReturnAllPatientsData()
    {
        var mockPatient = new OperationResult
        {
            Success = true,
            data = new List<Patients>
        {
            new Patients
            {
                UserId = 1,
                DateOfBirth = new DateOnly(1990, 1, 1),
                IsActive = true,
                EmergencyContactPhone = "1343244",
                BloodType = 'O',
                Address = "San Isidro",
                Allergies = "none",
                EmergencyContactName = "Andrea",
                InsuranceProviderID = 344,
                Gender = 'M',
                PhoneNumber = "8883423"
            }

        }
        };

        //act 
        _mockPatientServices.Setup(x => x.GetAllData()).ReturnsAsync(mockPatient);
        var result = await _mockPatientServices.Object.GetAllData();

        //Assert

        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task GetPatientByIdReturnPatientsById()
    {
        var mockPatient = new OperationResult
        {
            Success = true,
            data = new List<Patients>
            {
                new Patients
                {
                    UserId = 1,
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    IsActive = true,
                    EmergencyContactPhone = "1343244",
                    BloodType = 'O',
                    Address = "San Isidro",
                    Allergies = "none",
                    EmergencyContactName = "Andrea",
                    InsuranceProviderID = 344,
                }

            }
        };
        _mockPatientServices.Setup(x => x.GetById(1)).ReturnsAsync(mockPatient);
        var result = await _mockPatientServices.Object.GetById(1);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task AddPatientReturnPatientAdded()
    {
        var mockPatient = new OperationResult
        {
            Success = true,
            data = new List<Patients>
            {
                new Patients
                {
                    Address = "Calle 1",
                    IsActive = true,
                    EmergencyContactPhone = "3423432",
                    Allergies = "Aspirina",
                    BloodType = 'O',
                    DateOfBirth = new DateOnly(1993, 1, 14),
                    EmergencyContactName = "Beltre",
                    UserId = 20,
                    Gender = 'F',
                    InsuranceProviderID = 202,
                    PhoneNumber = "42236"
                }
            }
        };

        var AddPatientsDto = new AddPatientDto
        {
            Address = "Calle 1",
            IsActive = true,
            EmergencyContactPhone = "3423432",
            Allergies = "Aspirina",
            BloodType = 'O',
            DateOfBirth = new DateOnly(1993, 1, 14),
            EmergencyContactName = "Beltre",
            UserId = 20,
            Gender = 'M',
            InsuranceProviderID = 202,
            PhoneNumber = "42236"
        };
        _mockPatientServices.Setup(x => x.Add(It.IsAny<AddPatientDto>())).ReturnsAsync(mockPatient);
        var result = await _mockPatientServices.Object.Add(AddPatientsDto);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    [Fact]
    public async Task UpdatePatientReturnPatientUpdated()
    {
        var mockPatient = new OperationResult
        {
            Success = true,
            data = new List<Patients>
            {
                new Patients
                {
                    Address = "Calle 11",
                    IsActive = true,
                    EmergencyContactPhone = "787892",
                    Allergies = "Butter",
                    BloodType = 'A',
                    DateOfBirth = new DateOnly(2003, 10, 1),
                    EmergencyContactName = "Perez",
                    UserId = 40,
                    Gender = 'M',
                    InsuranceProviderID = 204,
                    PhoneNumber = "787892"
              }
            }
        };
        var UpdatePatientsDto = new UpdatePatientDto
    {
        Address = "Calle 11",
        IsActive = true,
        EmergencyContactPhone = "787892",
        Allergies = "Butter",
        BloodType = 'A',
        DateOfBirth = new DateOnly(2003, 10, 1),
        EmergencyContactName = "Perez",
        UserId = 40,
        Gender = 'M',
        InsuranceProviderID = 204,
        PhoneNumber = "787892"
    };
        _mockPatientServices.Setup(x => x.Update(It.IsAny<UpdatePatientDto>())).ReturnsAsync(mockPatient);
        var result = await _mockPatientServices.Object.Update(UpdatePatientsDto);
        Assert.NotNull(result);
        Assert.True(result.Success);


    }
    [Fact]

    public async Task DeletePatientReturnPatientDeleted()
    {
        var mockPatient = new OperationResult
        {
            Success = true,
            data = new List<Patients>
            {
                new Patients
                {
                    Address = "Calle 11",
                    IsActive = true,
                    EmergencyContactPhone = "787892",
                    Allergies = "Butter",
                    BloodType = 'A',
                    DateOfBirth = new DateOnly(2003, 10, 1),
                    EmergencyContactName = "Perez",
                    UserId = 40,
                    Gender = 'M',
                    InsuranceProviderID = 204,
                    PhoneNumber = "787892"
                }
            }
        };
        var DeletePatientsDto = new DeletePatientDto
        {
            UserId = 40
        };
        _mockPatientServices.Setup(x => x.Delete(It.IsAny<DeletePatientDto>())).ReturnsAsync(mockPatient);
        var result = await _mockPatientServices.Object.Delete(DeletePatientsDto);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    }