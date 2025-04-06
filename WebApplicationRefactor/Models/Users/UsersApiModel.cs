namespace WebApplicationRefactor.Models.Users
{
    public class UsersApiModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleID { get; set; }
    }
}
