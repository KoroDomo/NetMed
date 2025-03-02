

using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Application.Dtos.Doctors
{
 public class UpdateDoctorsDto : DoctorsDto
    {
        public int AvailabilityModeId { get; set; }

    }
}
