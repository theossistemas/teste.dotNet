using Theos.Challenge.Library.SharedKernel.Enums;

namespace Theos.Challenge.Library.SharedKernel.ValueObjects
{
    public class Genre
    {
        public EBookType Type { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}