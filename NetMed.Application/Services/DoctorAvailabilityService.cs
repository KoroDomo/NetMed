

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Persistence.Interfaces;

namespace NetMed.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly ILogger<DoctorAvailabilityService> _logger;
        private readonly IConfiguration _configuration;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository , ILogger<DoctorAvailabilityService> logger, IConfiguration configuration)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveAppointmentsDto TDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveAppointmentsDto TDto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(UpdateAppointmentsDto TDto)
        {
            throw new NotImplementedException();
        }
    }
}
