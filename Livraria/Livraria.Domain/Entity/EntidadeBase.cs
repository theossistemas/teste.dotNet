using System;

namespace Livraria.Domain.Entity
{
    public class EntidadeBase
    {
        public Guid Id { get; protected set; }
        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }

        public void SetCreatedDateTime(DateTime created)
        {
            Created = created;
        }

        public void SetUpdatedDateTime(DateTime updated)
        {
            Updated = updated;
        }
    }
}
