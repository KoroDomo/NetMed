
namespace Netmed.Model.Models
{
   public class RolesModel
    {
      public int RoleId { get; set; }

      public string RoleName { get; set; }

      public DateTime CreatedAt { get; set; }

      public DateTime? UpdatedAt { get; set; }

      public bool IsActive { get; set; }

    }
}
