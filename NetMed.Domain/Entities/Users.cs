using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{

    [Table("Users", Schema = "users")]
    public sealed class UsersModel : PersonEntity
    {
        [Key]
        [Required]
        public override int UserID { get; set; }
        public required string FirstName { get; set; }

        [Required]
        public required string Password { get; set; }
        public int RoleID { get; set; }
        


    }
}