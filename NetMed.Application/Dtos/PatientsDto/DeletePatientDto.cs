

namespace NetMed.Application.Dtos.PatientsDto
{
 public class DeletePatientDto : DtoBase
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
