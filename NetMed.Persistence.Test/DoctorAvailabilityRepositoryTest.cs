using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;
using System.Linq.Expressions;


namespace NetMed.Tests
{
    public class DoctorAvailabilityRepositoryTest
    {
        private readonly NetMedContext _context;
        private readonly LoggerSystem _logger;
        private Validations _validations;
        private readonly DoctorAvailabilityRepository _repository;
        private readonly IMessageService _messageService;


        public DoctorAvailabilityRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new NetMedContext(options);

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = new LoggerSystem(loggerFactory.CreateLogger<LoggerSystem>());
            _validations = new Validations();
            _messageService = new MessageService("messages.json");
            _repository = new DoctorAvailabilityRepository(_context, _logger, _messageService, _validations);
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldSaveDoctorAvailability()
        {
            // Arrange
            var doctorAvailability = new DoctorAvailability
            {
                Id = 0,
                DoctorID = 1,
                AvailableDate = DateOnly.Parse("2025-03-19"),
                StartTime = TimeOnly.Parse("09:30:00"),
                EndTime = TimeOnly.Parse("18:00:00")
            };

            // Act
            var result = await _repository.SaveEntityAsync(doctorAvailability);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Guardados con exito", result.Message);
            Assert.True(_context.DoctorAvailability.Any(a => a.DoctorID == 1));
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange
            DoctorAvailability doctorAvailability = null;

            // Act
            var result = await _repository.SaveEntityAsync(doctorAvailability);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La entidad no puede ser nula", result.Message);
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenDoctorAvailabilityExists()
        {
            // Arrange
            int availabilityID = 1;
            int doctorId = 1;
            DateOnly availableDate = DateOnly.Parse("2025-03-19");
            TimeOnly startTime = TimeOnly.Parse("09:15:00");
            TimeOnly endTime = TimeOnly.Parse("22:00:00");

            // Cita existente en la base de datos
            var doctorAvailability = new DoctorAvailability
            {
                Id = availabilityID,
                DoctorID = doctorId,
                AvailableDate = availableDate,
                StartTime = startTime,
                EndTime = endTime
            };
            _context.DoctorAvailability.Add(doctorAvailability);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.SaveEntityAsync(doctorAvailability);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El paciente ya tiene una cita programada con este doctor para esta fecha y hora", result.Message);
        }//Check
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenAppointmentDateIsInvalid()
        {
            //Arrange
            var doctorAvailability = new DoctorAvailability
            {
                Id = 0,
                DoctorID = 10,
                AvailableDate = DateOnly.Parse("2025-03-19"),
                StartTime = TimeOnly.Parse("09:30:00"),
                EndTime = TimeOnly.Parse("18:00:00")
            };

            // Act
            var result = await _repository.SaveEntityAsync(doctorAvailability);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha debe ser mayor a la fecha actual", result.Message);
        }//check
        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateDoctorAvailability()
        {
            // Arrange
            var doctorAvailability = new DoctorAvailability
            {
                Id = 1,
                DoctorID = 1,
                AvailableDate = DateOnly.Parse("2025-04-15")
            };
            _context.DoctorAvailability.Add(doctorAvailability);
            await _context.SaveChangesAsync();

            //Actualizar
            doctorAvailability.AvailableDate = DateOnly.Parse("2025-04-17");

            // Act
            var result = await _repository.UpdateEntityAsync(doctorAvailability);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Actualizados con exito", result.Message);
            Assert.Equal(DateOnly.Parse("2025-04-17"), _context.DoctorAvailability.First().AvailableDate);
        }
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange
            DoctorAvailability doctorAvailability = null;

            // Act
            var result = await _repository.UpdateEntityAsync(doctorAvailability);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La entidad no puede ser nula", result.Message);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess()
        {
            OperationResult result = new OperationResult();
            //arrange
            var doctorAvailability = new DoctorAvailability()
            {
                Id = 0,
                DoctorID = 10,
                AvailableDate = DateOnly.Parse("2025-03-19"),
                StartTime = TimeOnly.Parse("09:30:00"),
                EndTime = TimeOnly.Parse("18:00:00")
            };
            //act
            var datos = await _repository.GetAllAsync();
            //assert
            Assert.True(result.Success);
            Assert.Equal("Datos Obtenidos con exito", result.Message);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnError_WhenEntityIsNull()
        {
            //Arrange
            Expression<Func<DoctorAvailability, bool>> filter = null;

            //Act
            var result = await _repository.GetAllAsync(filter);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("La entidad no puede ser nula", result.Message);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnSeccess_WhenFiltreSuccessfully()
        {
            //Arrange
            Expression<Func<DoctorAvailability, bool>> filter = a => a.AvailableDate >= DateOnly.Parse("2025-03-19");

            //Act
            var result = await _repository.GetAllAsync(filter);

            //Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Obtenidos con exito", result.Message);
        }
        [Fact]
        public async Task ExistsAsync_ShouldReturnSeccess()
        {
            // Arrange
            var existingAvailabilityId = 7; //Check ID
            Expression<Func<DoctorAvailability, bool>> filter = (a => a.Id == existingAvailabilityId);

            // Act
            var result = await _repository.ExistsAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Entidad encontrada con éxito", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExists()
        {
            OperationResult result = new OperationResult();
            // Arrange
            int validId = 7; //Check Id

            // Act
            var datos = await _repository.GetEntityByIdAsync(validId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Obtenidos con exito", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnError_WhenIdIsNegativeOrZero()
        {
            OperationResult result = new OperationResult();
            //Arrange
            int Id = -1;
            int id = 0;

            // Act
            var datos = await _repository.GetEntityByIdAsync(Id);
            datos = await _repository.GetEntityByIdAsync(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnError_WhenIdNotExist()
        {
            OperationResult result = new OperationResult();
            // Arrange
            int nonExistentId = 99;

            // Act
            var datos = await _repository.GetEntityByIdAsync(nonExistentId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("No existe este registro en el sistema", result.Message);
        }       
        //[Fact]
        //public async Task RemoveAsync_ShouldReturnError_WhenIdIsNullOrWhiteSpace()
        //{
        //    // Arrange
        //    int invalidId = 0;

        //    // Act
        //    var result = await _repository.RemoveAsync(invalidId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.False(result.Success);
        //    Assert.Equal("El Id debe ser mayor que cero", result.Message);
        //}
        //[Fact]
        //public async Task RemoveAsync_ShouldReturnError_WhenIdIsNotInteger()
        //{
        //    // Arrange
        //    int invalidId = -1; 

        //    // Act
        //    var result = await _repository.RemoveAsync(invalidId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.False(result.Success);
        //    Assert.Equal("El Id debe ser mayor que cero", result.Message);
        //}
        //[Fact]
        //public async Task RemoveAsync_ShouldReturnSuccess_WhenIdExists()
        //{
        //    // Arrange
        //    int existingId = 2; 

        //    // Act
        //    var result = await _repository.RemoveAsync(existingId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal("Datos desactivados con exito", result.Message);
        //}

        [Fact]
        public async Task SetAvailabilityAsync_ShouldReturnError_WhenDoctorIdIsInvalid()
        {
            // Arrange
            int invalidDoctorId = 0;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);

            // Act
            var result = await _repository.SetAvailabilityAsync(invalidDoctorId, availableDate, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task SetAvailabilityAsync_ShouldReturnError_WhenTimeIsInvalid()
        {
            // Arrange
            int doctorId = 1;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly startTime = TimeOnly.Parse("22:00:00");
            TimeOnly endTime = TimeOnly.Parse("09:15:00");

            // Act
            var result = await _repository.SetAvailabilityAsync(doctorId, availableDate, startTime, endTime);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La hora de inicio debe ser anterior a la hora de finalización.", result.Message);
        }

        [Fact]
        public async Task SetAvailabilityAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange
            int doctorId = 1;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);

            // Act
            var result = await _repository.SetAvailabilityAsync(doctorId, availableDate, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Disponibilidad establecida correctamente.", result.Message);
        }
        [Fact]
        public async Task GetAvailabilityByDoctorAndDateAsync_ShouldReturnError_WhenDoctorIdIsInvalid()
        {
            // Arrange
            int invalidDoctorId = -5;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var result = await _repository.GetAvailabilityByDoctorAndDateAsync(invalidDoctorId, availableDate);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }

        [Fact]
        public async Task GetAvailabilityByDoctorAndDateAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            int doctorId = 1;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var result = await _repository.GetAvailabilityByDoctorAndDateAsync(doctorId, availableDate);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Disponibilidad obtenida con exito", result.Message);

        }
        [Fact]
        public async Task UpdateAvailabilityAsync_ShouldReturnError_WhenAvailabilityIdIsInvalid()
        {
            // Arrange
            int invalidAvailabilityId = 0;
            int doctorId = 1;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);

            // Act
            var result = await _repository.UpdateAvailabilityAsync(invalidAvailabilityId, doctorId, availableDate, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task RemoveAvailabilityAsync_ShouldReturnError_WhenAvailabilityIdIsInvalid()
        {
            // Arrange
            int invalidAvailabilityId = -1;

            // Act
            var result = await _repository.RemoveAvailabilityAsync(invalidAvailabilityId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task IsDoctorAvailableAsync_ShouldReturnFalse_WhenDoctorHasOverlappingAvailability()
        {
            // Arrange
            int doctorId = 1;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);

            var overlappingAvailability = new DoctorAvailability
            {
                DoctorID = doctorId,
                AvailableDate = availableDate,
                StartTime = new TimeOnly(9, 30),  
                EndTime = new TimeOnly(10, 30)
            };

            _context.DoctorAvailability.Add(overlappingAvailability);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.IsDoctorAvailableAsync(doctorId, availableDate, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El doctor no está disponible para este horario.", result.Message);
        }

        [Fact]
        public async Task UpdateAvailabilityInRealTimeAsync_ShouldReturnError_WhenDoctorIdIsInvalid()
        {
            // Arrange
            int invalidDoctorId = 0;
            var availableDate = DateOnly.FromDateTime(DateTime.Now);
            var startTime = new TimeOnly(10, 0);
            var endTime = new TimeOnly(12, 0);

            // Act
            var result = await _repository.UpdateAvailabilityInRealTimeAsync(invalidDoctorId, availableDate, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }


    }
}
