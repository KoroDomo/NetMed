
namespace NetMed.Application.Dtos
{
    public class DtoBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsActive {get; set; }
    }
}
