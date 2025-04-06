using NetMed.WebApi.Models.DoctorAvailability;

namespace WebApiApplication.Persistence.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        Task<List<DoctorAvailabilityModel>> GetAllDoctorAvailabilityAsync();
        Task<DoctorAvailabilityModel> GetDoctorAvailabilityByIdAsync(int id);
        Task<bool> CreateDoctorAvailabilityAsync(DoctorAvailabilityModelSave model);
        Task<bool> UpdateDoctorAvailabilityAsync(DoctorAvailabilityModelUpdate model);
        Task<bool> DeleteDoctorAvailabilityAsync(DoctorAvailabilityModelRemove model);

    }
}
