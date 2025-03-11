

using NetMed.Application.Base;
using NetMed.Application.Dtos.Doctors;
using NetMed.Domain.Base;

namespace NetMed.Application.Contracts
{
    public interface IDoctorsServices : IBaseService<AddDoctorsDto, UpdateDoctorsDto, DeleteDoctorDto>
    {
    }

}
