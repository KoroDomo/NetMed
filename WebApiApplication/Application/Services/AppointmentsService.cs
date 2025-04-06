using NetMed.WebApi.Models.Appointments;
using WebApiApplication.Application.Interfaces;
using WebApiApplication.Persistence.Interfaces;


namespace WebApiApplication.Application.Services
{
    
    public class AppointmentService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _repo;

        public AppointmentService(IAppointmentsRepository repo)
        {
            _repo = repo;
        }

        public Task<List<AppointmentsModel>> GetAllAsync() => _repo.GetAllAppointmentsAsync();
        public Task<AppointmentsModel> GetByIdAsync(int id) => _repo.GetAppointmentByIdAsync(id);
        public Task<bool> SaveAsync(AppointmentsModelSave model) => _repo.CreateAppointmentAsync(model);
        public Task<bool> UpdateAsync(AppointmentsModelUpdate model) => _repo.UpdateAppointmentAsync(model);
        public Task<bool> RemoveAsync(AppointmentsModelRemove model) => _repo.DeleteAppointmentAsync(model);
    }
}
