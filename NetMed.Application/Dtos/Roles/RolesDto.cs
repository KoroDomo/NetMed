
namespace NetMed.Application.Dtos.Roles
{
   public class RolesDto : DtoBase
    {
        public int RolesId { get; set; }

        public required string RoleName { get; set; }
    }
}
