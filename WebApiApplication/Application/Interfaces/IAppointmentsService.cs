using NetMed.WebApi.Models.Appointments;


namespace WebApiApplication.Application.Interfaces
{
    public interface IAppointmentsService 
    {
        Task<List<AppointmentsModel>> GetAllAsync();
        Task<AppointmentsModel> GetByIdAsync(int id);
        Task<bool> SaveAsync(AppointmentsModelSave model);
        Task<bool> UpdateAsync(AppointmentsModelUpdate model);
        Task<bool> RemoveAsync(AppointmentsModelRemove model);
    }
}
