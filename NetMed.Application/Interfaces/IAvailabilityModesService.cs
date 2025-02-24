

using NetMed.Application.Base;
using NetMed.Application.Dtos.Specialties;

namespace NetMed.Application.Interfaces
{
    public interface IAvailabilityModesService : IBaseService<SaveSpecialtiesDto, UpdateSpecialtiesDto, RemoveSpecialtiesDto>
    {
    }
}
