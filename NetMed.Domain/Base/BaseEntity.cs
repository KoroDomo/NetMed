
namespace NetMed.Domain.Base
{
    public abstract class BaseEntity<Ttype> : AptEntity
    {
        public abstract Ttype UserID{ get; set; }
    }
}
