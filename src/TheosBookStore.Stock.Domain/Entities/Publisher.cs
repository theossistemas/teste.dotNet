using TheosBookStore.LibCommon.Entities;
using TheosBookStore.Stock.Domain.Validations;

namespace TheosBookStore.Stock.Domain.Entities
{
    public class Publisher : Entity
    {
        public string Name { get; protected set; }
        public Publisher(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            _validationResult = new PublisherValidations().Validate(this);
            return _validationResult.IsValid;
        }
    }
}
