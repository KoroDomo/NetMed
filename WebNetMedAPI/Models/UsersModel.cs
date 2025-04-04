namespace NetMed.Web.Models
{
    public class UsersModel
    {

        public int Id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public  string? email { get; set; }
        public  string? password { get; set; }
        public int roleID { get; set; }
    } 
}
