using System.Collections.Generic;
using WeirdFlex.Common.Model;

namespace WeirdFlex.Business.Interfaces
{
    public interface ISupportSorting
    {
        ICollection<SortItem>? Sorting { get; }
    }
}
