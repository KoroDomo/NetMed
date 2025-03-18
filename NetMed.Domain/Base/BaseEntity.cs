
using NetMed.Domain.Base;

namespace NetMed.Domain.Base
{
    public abstract class BaseEntity<TKey> : AptEntity
    {
        public abstract TKey UserId { get; set; }
    }
}

