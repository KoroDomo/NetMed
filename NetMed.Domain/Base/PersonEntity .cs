
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Base
{
    public abstract class PersonEntity : BaseEntity<int>
    {
        [Required]
        public string? Email { get; set; }
  
        public string? Address { get; set; }
       
        public string? PhoneNumber { get; set; }
        public required string LastName { get; set; }
    }

}
