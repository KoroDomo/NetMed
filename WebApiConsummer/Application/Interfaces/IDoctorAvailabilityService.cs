
using NetMed.WebApi.Models.DoctorAvailability;


namespace WebApiApplication.Application.Interfaces
{
    public interface IDoctorAvailabilityService 
    {
        Task<List<DoctorAvailabilityModel>> GetAllAsync();
        Task<DoctorAvailabilityModel> GetByIdAsync(int id);
        Task<bool> SaveAsync(DoctorAvailabilityModelSave model);
        Task<bool> UpdateAsync(DoctorAvailabilityModelUpdate model);
        Task<bool> RemoveAsync(DoctorAvailabilityModelRemove model);
    }
}
