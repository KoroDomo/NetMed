using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Core.Entity;
using WebApplicationRefactor.Models.Doctors;

namespace WebApplicationRefactor.Application.Contracts
{
    public interface IDoctorServices : IBaseAppService<DoctorsApiModel, DoctorsApiModel, DoctorsApiModel>
    {
        
    }
}
