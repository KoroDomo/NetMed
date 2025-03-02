
using System.ComponentModel.DataAnnotations;

namespace NetMed.Application.Dtos.UsersDto
{
    public class UserDto : DtoBase
    {
        public required string FirstName { get; set; }
        public int RoleID { get; set; }

        public string? Email { get; set; }

        public required string  Password { get; set; }

        public required string LastName { get; set; }
    }
}
