using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Patients;

namespace WebApplicationRefactor.Application.Contracts
{
    public interface IPatientsService : IBaseAppService<PatientsApiModel, PatientsApiModel, PatientsApiModel>
    {
    }
}
