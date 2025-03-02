

namespace NetMed.Application.Dtos.Doctors
{
    public class DeleteDoctorDto  : DoctorsDto
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
