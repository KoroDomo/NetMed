
namespace NetMed.Application.Dtos.Roles
{
   public class RolesDto : DtoBase
    {
        public int id { get; set; }

        public required string RoleName { get; set; }
    }
}
