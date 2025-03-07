using NetMed.Application.Base;
using NetMed.Application.Dtos.Appointments;
using NetMed.Application.Dtos.DoctorAvailability;

namespace NetMed.Application.Interfaces
{
    public interface IDoctorAvailabilityService : IBaseService<SaveDoctorAvailabilityDto, UpdateDoctorAvailabilityDto, RemoveDoctorAvailabilityDto>
    {

    }
}
