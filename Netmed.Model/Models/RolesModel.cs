
namespace NetMed.Model.Models
{
   public class RolesModel
    {
      public int RoleId { get; set; }

        //Tener pendiente el required ya que me puede dar probleamas a la hora de copilar
      public required string  RoleName { get; set; }

      public DateTime CreatedAt { get; set; }

      public DateTime? UpdatedAt { get; set; }

      public bool IsActive { get; set; }

    }
}
