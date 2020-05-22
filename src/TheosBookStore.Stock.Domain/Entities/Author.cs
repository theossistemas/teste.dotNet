using TheosBookStore.LibCommon.Entities;

namespace TheosBookStore.Stock.Domain.Entities
{
    public class Author : Entity
    {
        public string Name { get; protected set; }
        public Author(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
