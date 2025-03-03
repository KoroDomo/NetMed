

namespace NetMed.Application.Dtos.Status
{
    public class DeleteStatusDto : DtoBase
    {
        public int StatusId { get; set; }

        public bool Deleted { get; set; }

    }
}
