
namespace NetMed.Application.Dtos
{
    public class DtoBase
    {
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
        public int ChangeUserID { get; set; }
    }
}
