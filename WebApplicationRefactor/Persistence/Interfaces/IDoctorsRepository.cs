using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Doctors;
namespace NetMed.WebApplicationRefactor.Persistence.Interfaces
{
    public interface IDoctorRepository : IBaseAppService<DoctorsApiModel, DoctorsApiModel, DoctorsApiModel>
    {
        Task<IEnumerable<DoctorsApiModel>> GetDoctorsBySpecialtyAsync(string specialty);
    }

}

