
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMed.Domain.Base
{
    public abstract class PersonEntity : BaseEntity<int>
    {
        
        public string? Email { get; set; }
       
        public required string LastName { get; set; }
    }

}
