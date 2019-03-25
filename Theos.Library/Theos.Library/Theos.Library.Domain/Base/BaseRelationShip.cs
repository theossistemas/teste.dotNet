using System;

namespace Theos.Library.Domain.Base
{
    public class BaseRelationShip<T> : BaseEntity
    {
        public Guid KeyId { get; set; }
        public virtual T Key { get; set; }
    }
}
