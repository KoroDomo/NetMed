

namespace NetMed.Application.Dtos.Patients
{
 public class DeletePatientDto : DtoBase
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
