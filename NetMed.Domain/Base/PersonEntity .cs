
namespace NetMed.Domain.Base
{
    public abstract class PersonEntity : BaseEntity<int>
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

}
