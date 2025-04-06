using WebApplicationRefactor.Application.BaseApp;

using WebApplicationRefactor.Models.Patients;

namespace NetMed.WebApplicationRefactor.Persistence.Interfaces
{
    public interface IPatientsRepository : IBaseAppService<PatientsApiModel, PatientsApiModel , PatientsApiModel>
    {
    }
}
