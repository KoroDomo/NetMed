using NetMed.Application.Base;
using WebApplicationRefactor.Models.Patients;
using WebApplicationRefactor.Application.Contracts;

namespace WebApplicationRefactor.ServicesApi.Interface
{
    public interface IPatientsService : IBaseService<PatientsApiModel, PatientsApiModel, PatientsApiModel>
    {
    }
}