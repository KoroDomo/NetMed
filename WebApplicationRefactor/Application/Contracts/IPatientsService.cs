using NetMed.Application.Base;
using WebApplicationRefactor.Application.BaseApp;
using WebApplicationRefactor.Models.Patients;


namespace WebApplicationRefactor.ServicesApi.Interface
{
    public interface IPatientsService : IBaseAppService<PatientsApiModel, PatientsApiModel, PatientsApiModel>
    {
    }
}