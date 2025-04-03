namespace NetMed.Web.Models
{
    public class UsersModel
    {

        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string? Email { get; set; }
        public required string Password { get; set; }
        public int RoleID { get; set; }
    }
}
