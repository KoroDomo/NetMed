
using NetMed.Application.Base;
using NetMed.Application.Dtos.Specialties;

namespace NetMed.Application.Interfaces
{
    public interface IMedicalRecordsService : IBaseService<SaveSpecialtiesDto, UpdateSpecialtiesDto, RemoveSpecialtiesDto>
    {

    }
}
