using NetMed.WebApi.Models.Appointments;


namespace WebApiApplication.Persistence.Interfaces
{
    public interface IAppointmentsRepository 
    {
        Task<List<AppointmentsModel>> GetAllAppointmentsAsync();
        Task<AppointmentsModel> GetAppointmentByIdAsync(int id);
        Task<bool> CreateAppointmentAsync(AppointmentsModelSave model);
        Task<bool> UpdateAppointmentAsync(AppointmentsModelUpdate model);
        Task<bool> DeleteAppointmentAsync(AppointmentsModelRemove model);

    }
}
