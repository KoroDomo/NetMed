

using NetMed.Application.Base;
using NetMed.Application.Dtos.AvailabilityModes;
using NetMed.Application.Dtos.Specialties;

namespace NetMed.Application.Interfaces
{
    public interface IAvailabilityModesService : IBaseService<SaveAvailabilityModesDto, UpdateAvailabilityModesDto, RemoveAvailabilityModesDto>
    {
    }
}
