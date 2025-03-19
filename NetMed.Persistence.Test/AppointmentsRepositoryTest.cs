using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Infraestructure.Validators;
using NetMed.Persistence.Context;
using NetMed.Persistence.Repositories;
using System.Linq.Expressions;


namespace NetMed.Tests
{
    public class AppointmentsRepositoryTests
    {
        private readonly NetMedContext _context;
        private readonly LoggerSystem _logger;
        private Validations _validations;
        private readonly AppointmentsRepository _repository;
        private readonly IMessageService _messageService;


        public AppointmentsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<NetMedContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            _context = new NetMedContext(options);

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = new LoggerSystem(loggerFactory.CreateLogger<LoggerSystem>());
            _validations = new Validations();
            _messageService = new MessageService("messages.json");
            _repository = new AppointmentsRepository(_context,_logger, _validations, _messageService);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldSaveAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1,
            };

            // Act
            var result = await _repository.SaveEntityAsync(appointment);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Guardados con exito", result.Message);
            Assert.True(_context.Appointments.Any(a => a.PatientID == 1));
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange
            Appointments nullAppointment = null;

            // Act
            var result = await _repository.SaveEntityAsync(nullAppointment);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La entidad no puede ser nula", result.Message);
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenAppointmentExists()
        {
            // Arrange
            int patientId = 1;
            int doctorId = 1;
            DateTime appointmentDate = DateTime.Now.AddDays(1);
            int statusId = 1;

            // Cita existente en la base de datos
            var existingAppointment = new Appointments
            {
                PatientID = patientId,
                DoctorID = doctorId,
                AppointmentDate = appointmentDate,
                StatusID = statusId
            };
            _context.Appointments.Add(existingAppointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.SaveEntityAsync(existingAppointment);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El paciente ya tiene una cita programada con este doctor para esta fecha y hora", result.Message);
        }
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenAppointmentDateIsInvalid()
        {
            //Arrange
            var appointments = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(-1),
                StatusID = 1,
            };

            // Act
            var result = await _repository.SaveEntityAsync(appointments);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha debe ser mayor a la fecha actual", result.Message);
        }
        [Fact]
        public async Task UpdateEntityAsync_ShouldUpdateAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            //Actualizar
            appointment.AppointmentDate = DateTime.Now.AddDays(2);

            // Act
            var result = await _repository.UpdateEntityAsync(appointment);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Actualizados con exito", result.Message);
            Assert.Equal(DateTime.Now.AddDays(2).Date, _context.Appointments.First().AppointmentDate.Date);
        }
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange
            Appointments appointments = null;

            // Act
            var result = await _repository.UpdateEntityAsync(appointments);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La entidad no puede ser nula", result.Message);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnSeccess()
        {
            //Arrange
            var appointments = new Appointments()
            {
                Id = 0,
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = 1,
            };

            //Act
            var result = await _repository.GetAllAsync();

            //Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Obtenidos con exito", result.Message);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnError_WhenEntityIsNull()
        {
            //Arrange
            Expression<Func<Appointments, bool>> filter = null;

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
            Expression<Func<Appointments, bool>> filter = a => a.AppointmentDate >= DateTime.Now;

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
            var existingAppointmentId = 7;
            Expression<Func<Appointments, bool>> filter = (a => a.Id == existingAppointmentId);

            // Act
            var result = await _repository.ExistsAsync(filter);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Entidad encontrada con éxito", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExists()
        {
            // Arrange
            int validId = 7;

            // Act
            var result = await _repository.GetEntityByIdAsync(validId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos Obtenidos con exito", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnError_WhenIdIsNegativeOrZero()
        {
            //Arrange
            int Id = -1;
            int id = 0;

            // Act
            var result = await _repository.GetEntityByIdAsync(Id);
            result = await _repository.GetEntityByIdAsync(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnError_WhenIdNotExist()
        {
            // Arrange
            int nonExistentId = 99;

            // Act
            var result = await _repository.GetEntityByIdAsync(nonExistentId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("No existe este registro en el sistema", result.Message);
        }
        [Fact]
        public async Task RemoveAsync_ShouldReturnSuccess_WhenIdExists()
        {
            // Arrange
            int Id = 7;

            // Act
            var result = await _repository.RemoveAsync(Id);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Datos desactivados con exito", result.Message);
        }
        [Fact]
        public async Task RemoveAsync_ShouldReturnError_WhenIdIsNegativeOrZero()
        {
            //Arrange
            int Id = -1;
            int id = 0;

            // Act
            var result = await _repository.RemoveAsync(Id);
            result = await _repository.RemoveAsync(id);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task RemoveAsync_ShouldReturnError_WhenIdNotExist()
        {
            // Arrange
            int nonExistentId = 99;

            // Act
            var result = await _repository.RemoveAsync(nonExistentId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("No existe este registro en el sistema", result.Message);
        }
        [Fact]
        public async Task CreateAppointmentAsync_ShouldNotAllowDuplicateAppointments()
        {
            // Arrange
            int appointmentId = 0;
            int patientId = 1;
            int doctorId = 1;
            DateTime appointmentDate = DateTime.Now.AddDays(1);
            int statusId = 1;

            // Agregar una cita existente
            var existingAppointment = new Appointments
            {
                PatientID = patientId,
                DoctorID = doctorId,
                AppointmentDate = appointmentDate,
                StatusID = statusId
            };
            _context.Appointments.Add(existingAppointment);
            await _context.SaveChangesAsync();

            // Act - Intentar crear una cita duplicada
            var result = await _repository.CreateAppointmentAsync(appointmentId, patientId, doctorId, appointmentDate, statusId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El paciente ya tiene una cita programada con este doctor para esta fecha y hora", result.Message);
        }
        [Fact]
        public async Task CreateAppointmentAsync_ShouldCreateAppointmentSuccessfully()
        {

            // Arrange
            int appointmentId = 0;
            int patientId = 1;
            int doctorId = 1;
            DateTime appointmentDate = DateTime.Now.AddDays(1);
            int statusId = 1;

            // Act
            var result = await _repository.CreateAppointmentAsync(appointmentId, patientId, doctorId, appointmentDate, statusId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita creada exitosamente.", result.Message);
            Assert.NotNull(result.Data);
            Assert.True(_context.Appointments.Any(a => a.PatientID == patientId && a.DoctorID == doctorId));
        }
        [Fact]
        public async Task CreateAppointmentAsync_ShouldFailForInvalidDate()
        {
            // Arrange
            int appointmentId = 0;
            int patientId = 1;
            int doctorId = 1;
            DateTime invalidAppointmentDate = DateTime.Now.AddDays(-1);
            int statusId = 1;

            // Act
            var result = await _repository.CreateAppointmentAsync(appointmentId, patientId, doctorId, invalidAppointmentDate, statusId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha debe ser mayor a la fecha actual", result.Message);
        }      
        [Fact]
        public async Task GetAppointmentsByPatientAsync_ShouldReturnAppointments()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByPatientAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita encontrada con exito", result.Message);
            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task GetAppointmentsByPatientAsync_ShouldReturnError_WhenPatientIdIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = -1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByPatientAsync(-1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);            
        }      
        [Fact]
        public async Task GetAppointmentsByPatientAsync_ShouldReturnError_WhenPatientNoExist()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 11,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByPatientAsync(11);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El paciente no existe en el sistema", result.Message);

        }
        [Fact]
        public async Task GetAppointmentsByDoctorAsync_ShouldReturnAppointments()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 10,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDoctorAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita encontrada con exito", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDoctorAsync_ShouldReturnError_WhenPatientIdIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = -1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDoctorAsync(-1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDoctorAsync_ShouldReturnError_WhenDoctorNoExist()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 18,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDoctorAsync(18);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("No existe este registro en el sistema", result.Message);

        }
        [Fact]
        public async Task GetAppointmentsByStatusAsync_ShouldReturnAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                Id = 0,
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now,
                StatusID = 2
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByStatusAsync(appointment.StatusID);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita encontrada con exito", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByStatusAsync_ShouldReturnError_WhenIdIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                StatusID = -2    
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByStatusAsync(-2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDateAsync_ShouldReturnAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                Id = 0,
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now,
                StatusID = 2
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDateAsync(appointment.AppointmentDate);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Lista de todas las citas en esta fecha", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDateAsync_ShouldReturnError_WhenDateIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(-21),
                StatusID = -2
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDateAsync(DateTime.Now.AddDays(-21));

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha debe ser mayor a la fecha actual", result.Message);
        }
        [Fact]
        public async Task CancelAppointmentAsync_ShouldCancelAppointment()
        {
            // Arrange
            var appointment = new Appointments
            {
                Id = 0,
                PatientID = 1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now,
                StatusID = 2 
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CancelAppointmentAsync(appointment.Id);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita cancelada con exito.", result.Message);
            Assert.Equal(2, _context.Appointments.First().StatusID); 
        }       
        [Fact]
        public async Task CancelAppointmentAsync_ShouldReturnError_WhenIdIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = -1,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CancelAppointmentAsync(-1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("El Id debe ser mayor que cero", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDoctorAndDateAsync_ShouldAppointments()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 10,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDoctorAndDateAsync(1, DateTime.Now.AddDays(1));

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Cita encontrada con exito", result.Message);
        }
        [Fact]
        public async Task GetAppointmentsByDoctorAndDateAsync_ShouldReturnFailure_WhenAppointmentDateIsInvalid()
        {
            // Arrange
            var appointment = new Appointments
            {
                PatientID = 10,
                DoctorID = 1,
                AppointmentDate = DateTime.Now.AddDays(-1)
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAppointmentsByDoctorAndDateAsync(1, DateTime.Now.AddDays(-1));

            // Assert
            Assert.False(result.Success);
            Assert.Equal("La fecha debe ser mayor a la fecha actual", result.Message);
        }
    }
}
