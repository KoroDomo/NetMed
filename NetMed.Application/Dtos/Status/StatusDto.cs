
namespace NetMed.Application.Dtos.Status
{
    public class StatusDto : DtoBase
    {
        public int StatusID { get; set; }

        public required string StatusName { get; set; }
    }
}
