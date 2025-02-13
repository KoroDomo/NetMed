using NetMed.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetMed.Domain.Entities
{
    public sealed class Users : PersonEntity
    {
      

        [Column("UserID")]
        [Key]
        public override int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public int RoleID { get; set; }
        public DateTime RegisterDate { get; set; }

        public required string PasswordHash { get; set; }
    }
}