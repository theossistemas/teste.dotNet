using TheosBookStore.LibCommon.ValueObjects;
using TheosBookStore.Stock.Domain.Validations;

namespace TheosBookStore.Stock.Domain.ValueObjects
{
    public class ISBN : ValueObject<ISBN>
    {
        public string Value { get; protected set; }
        public ISBN(string isbn)
        {
            Value = isbn;
        }
        protected override bool EqualsCore(ISBN other)
        {
            return other.Value == this.Value;
        }

        protected override int GetHashCodeCore()
        {
            return GetType().GetHashCode() * 42
                + this.Value.GetHashCode();
        }

        public override bool IsValid()
        {
            _validationResult = new ISBNValidations().Validate(this);
            return _validationResult.IsValid;
        }
    }
}
