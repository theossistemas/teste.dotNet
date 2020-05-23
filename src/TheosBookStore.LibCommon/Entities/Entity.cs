using FluentValidation.Results;

namespace TheosBookStore.LibCommon.Entities
{
    public abstract class Entity
    {
        public ValidationResult ValidationResult { get => _validationResult; }
        protected ValidationResult _validationResult = new ValidationResult();


        public int Id { get; protected set; }

        public void DefineId(int id)
        {
            this.Id = id;
        }

        public abstract bool IsValid();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var compareTo = obj as Entity;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 42) + Id.GetHashCode();
        }

        // public override string ToString()
        // {
        //     return $"{this.GetType().Name} Id[{Id.ToString()}]";
        // }
    }
}
