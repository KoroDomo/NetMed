
using NetMed.Application.Base;
using NetMed.Application.Dtos.Appointments;

namespace NetMed.Application.Interfaces
{
    public interface IAppointmentsService : IBaseService<SaveAppointmentsDto,UpdateAppointmentsDto,RemoveAppointmentsDto>
    {
        
    }
}
