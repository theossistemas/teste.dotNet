using System;

namespace Domain
{
    public abstract class BaseDomain : BaseDomain<Guid>
    {
    }

    public abstract class BaseDomain<TPK> where TPK : IComparable
    {
        public virtual TPK ID { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Updated { get; set; }
        public virtual bool Active { get; set; }
    }
}
