
namespace NetMed.Application.Dtos
{
    public class DtoBase
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime updatedAt { get; set; }

        public bool IsActive {get; set; }
    }
}
