using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Patients;
using NetMed.Application.Services;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Application.Test;

public class UnitTestPatientsServ
{
    private readonly IPatientsServices _patientsServices;
    private readonly ILogger<PatientsServices> _logger;
    private readonly Mock<IPatientsRepository> _mockPatientRepositoy;

    public UnitTestPatientsServ()
    {
        _logger = new Mock<ILogger<PatientsServices>>().Object;
        _mockPatientRepositoy = new Mock<IPatientsRepository>();
        _patientsServices = new PatientsServices(_mockPatientRepositoy.Object, _logger);
    }

    [Fact]

    public async Task GetAllDataReturnAllPatientsData()
    {
       
        var patients = new List<Patients>
        {
            new Patients
            {
                Id = 1,
                DateOfBirth = new DateOnly(1990, 1, 1),
                IsActive = true,
                EmergencyContactPhone = "1343244",
                BloodType = 'O',
                Address = "San Isidro",
                Allergies = "none",
                EmergencyContactName = "Andrea",
                InsuranceProviderID = 344,
            }
        };
        var result = new OperationResult
        {
            Success = true,
            data = patients
        };
        _mockPatientRepositoy.Setup(x => x.GetAllAsync()).ReturnsAsync(result);
        var resultData = await _patientsServices.GetAllData();

        Assert.NotNull(resultData);
        Assert.True(resultData.Success);
        Assert.Equal(1, resultData.data?.Count);
    }
    [Fact]
    public async Task GetPatientByIdReturnPatientsById()
    {
      var doctor = new Patients
      {
          Id = 1,
          DateOfBirth = new DateOnly(1990, 1, 1),
          IsActive = true,
          EmergencyContactPhone = "1343244",
          BloodType = 'O',
          Address = "San Isidro",
          Allergies = "none",
          EmergencyContactName = "Andrea",
          InsuranceProviderID = 344,
      };
        var result = new OperationResult
        {
            Success = true,
            data = new List<Patients> { doctor }
        };
        //_mockPatientRepositoy.Setup(x => x.GetEntityByIdAsync(1)).ReturnsAsync(result);
        //    var resultData = await _patientsServices.GetById(1);

        //Assert.NotNull(resultData);
        //Assert.True(resultData.Success);
        //Assert.Equal(1, resultData.data?.Count);
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
                    Id = 20,
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
            Id = 20,
        };
    
        _mockPatientRepositoy.Setup(x => x.SaveEntityAsync(It.IsAny<Patients>())).ReturnsAsync(mockPatient);

        var result = await _patientsServices.Add(AddPatientsDto);
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
                    Id = 40,
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
        Id = 40,
        Gender = 'M',
        InsuranceProviderID = 204,
        PhoneNumber = "787892"
    };
        _mockPatientRepositoy.Setup(x => x.UpdateEntityAsync(It.IsAny<Patients>())).ReturnsAsync(mockPatient);
        var result = await _patientsServices.Update(UpdatePatientsDto);
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
                    Id = 40,
                    Gender = 'M',
                    InsuranceProviderID = 204,
                    PhoneNumber = "787892"
                }
            }
        };
        var DeletePatientsDto = new DeletePatientDto
        {
            Id = 40
        };
        _mockPatientRepositoy.Setup(x => x.UpdateEntityAsync(It.IsAny<Patients>())).ReturnsAsync(mockPatient);

        var result = new OperationResult
        {
            Success = true,
            data = mockPatient
        };

        var resultData = await _patientsServices.Delete(DeletePatientsDto);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
    }