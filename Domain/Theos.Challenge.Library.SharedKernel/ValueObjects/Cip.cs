namespace Theos.Challenge.Library.SharedKernel.ValueObjects
{
    /// <summary>
    /// This Value Object representes Cataloging In Publication Data Block
    /// aka CIP
    /// Only this case, CIP shouldn't use Identifiers Field and Classification Field
    /// Identifier field should be used on Book's class
    /// </summary>
    public sealed class Cip
    {
        public Cip(string names, string title, string description, string subjects)
        {
            Names = names;
            Title = title;
            Description = description;
            Subjects = subjects;
        }

        public string Names { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Subjects { get; private set; }

        
    }
}