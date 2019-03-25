using System;
using Theos.Library.CrossCutting.Filter.Base;

namespace Theos.Library.CrossCutting.Filter.Book
{
    public class BookFilter : BaseFilter
    {
        public BookFilter(string term, bool? ascending, BookOrdination? ordination)
        {
            if (!string.IsNullOrEmpty(term))
                Term = term.ToUpper();

            Ascending = ascending ?? true;
            Ordination = ordination ?? BookOrdination.Title;
        }

        public BookOrdination Ordination { get; set; }
        public bool Ascending { get; set; }
    }

    [Flags]
    public enum BookOrdination
    {
        Title,
        Author,
        Description
    }
}