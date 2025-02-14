using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Domain.Base;
using NetMed.Domain.Entities;
using NetMed.Persistence.Base;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;

namespace NetMed.Persistence.Repositories
{
    public class AppointmentsRepository : BaseRepository<Appointments>, IAppointmentsRespository
    {
        private readonly NetMedContext _context;
        private readonly ILogger<AppointmentsRepository> _logger;
        private readonly IConfiguration _configuration;

        public AppointmentsRepository(NetMedContext context, ILogger<AppointmentsRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<OperationResult> CancelAppointmentAsync(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> CreateAppointmentAsync(int patientId, int doctorId, DateOnly appointmentDate)
        {
            throw new NotImplementedException();
        }

        public Task<Appointments> GetAppointmentByIdAsync(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByDateAsync(DateOnly appointmentDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByDoctorAndDateAsync(int doctorId, DateOnly appointmentDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByPatientAndDateAsync(int patientId, DateOnly appointmentDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByPatientAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByStatusAsync(int statusId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateAppointmentStatusAsync(int appointmentId, int statusId)
        {
            throw new NotImplementedException();
        }
    }
}
