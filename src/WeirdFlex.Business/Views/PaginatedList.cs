using System.Collections.Generic;
using WeirdFlex.Common.Model;

namespace WeirdFlex.Business.Views
{
    public class PaginatedList<T> : List<T>
    {
        public const int DefaultPageSize = 10;

        public PaginatedList() : this(new List<T>())
        {

        }

        public PaginatedList(IList<T> items, PageInfo? currentPageInfo = null)
        {
            PageInfo = currentPageInfo ?? new PageInfo(1) { PageSize = DefaultPageSize };

            AddRange(items);
        }

        public PageInfo PageInfo { get; }
    }
}
