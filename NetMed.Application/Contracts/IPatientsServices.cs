
using NetMed.Application.Dtos;
using NetMed.Application.Base;
using NetMed.Application.Dtos.Patients;
using NetMed.Domain.Base;

namespace NetMed.Application.Contracts
{
    public interface IPatientsServices : IBaseService<AddPatientDto,UpdatePatientDto, DeletePatientDto>
    {
        
    }
}
