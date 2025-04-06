using NetMed.WebApi.Models.DoctorAvailability;
using WebApiApplication.Application.Interfaces;
using WebApiApplication.Persistence.Interfaces;

namespace WebApiApplication.Application.Services
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _repo;
        public DoctorAvailabilityService(IDoctorAvailabilityRepository repository)
        {
            _repo = repository;
        }

        public Task<List<DoctorAvailabilityModel>> GetAllAsync()
            => _repo.GetAllDoctorAvailabilityAsync();

        public Task<DoctorAvailabilityModel> GetByIdAsync(int id)
            => _repo.GetDoctorAvailabilityByIdAsync(id);

        public Task<bool> SaveAsync(DoctorAvailabilityModelSave model)
            => _repo.CreateDoctorAvailabilityAsync(model);

        public Task<bool> UpdateAsync(DoctorAvailabilityModelUpdate model)
            => _repo.UpdateDoctorAvailabilityAsync(model);
        public Task<bool> RemoveAsync(DoctorAvailabilityModelRemove model)
            => _repo.DeleteDoctorAvailabilityAsync(model);
    }

}

