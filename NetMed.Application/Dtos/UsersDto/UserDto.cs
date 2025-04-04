
using System.ComponentModel.DataAnnotations;

namespace NetMed.Application.Dtos.UsersDto
{
    public class UserDto : DtoBase
    {

        public string? FirstName { get; set; }
        public int RoleID { get; set; }

        public string? Email { get; set; }

        public  string?  Password { get; set; }

        public  string? LastName { get; set; }


    }
}
