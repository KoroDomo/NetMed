

namespace NetMed.Model.Models
{
    public class SpecialtiesModel
    {
        public int Id { get; set; }
        public string SpecialtyName { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
