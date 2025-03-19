
namespace NetMed.Domain.Base
{
    public abstract class PersonEntity : BaseEntity<int>
    {
        public string Email { get; set; }
        public string Address { get; set; }
    }

}
