using Theos.Challenge.Library.SharedKernel.Enums;

namespace Theos.Challenge.Library.SharedKernel.ValueObjects
{
    public class Identifier
    {
        public Identifier(long? number, EIdentifierType type)
        {
            Number = number;
            Type = type;
        }

        public long? Number { get; private set; }
        public EIdentifierType Type { get; private set; }
    }
}