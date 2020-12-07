using System.Collections.Generic;
using WeirdFlex.Common.Model;

namespace WeirdFlex.Business.Interfaces
{
    public interface ISupportFiltering
    {
        ICollection<FilterItem>? Filter { get; }
    }
}
