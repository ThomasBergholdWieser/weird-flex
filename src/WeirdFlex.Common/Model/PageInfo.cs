using System;
using System.Collections.Generic;
using System.Linq;

namespace WeirdFlex.Common.Model
{
    public sealed class PageInfo
    {
        public int? PageIndex { get; }

        public int? PageSize { get; set; }

        public int? TotalRowCount { get; }

        public PageInfo(int? pageIndex, int? totalRowCount = null)
        {
            PageIndex = pageIndex;
            TotalRowCount = totalRowCount;
        }

        public int TotalPageCount => (TotalRowCount.GetValueOrDefault() - 1) / PageSize.GetValueOrDefault(1) + 1;

        public const int VisiblePagePadding = 5;

        public IEnumerable<int> PageSpan
        {
            get
            {
                var minPage = 1;
                var maxPage = TotalPageCount;

                var currPage = PageIndex.GetValueOrDefault(minPage);
                var currStart = Math.Max(1, currPage - VisiblePagePadding);
                var totalSpan = VisiblePagePadding * 2 + 1;

                if (maxPage - currStart < totalSpan)
                {
                    currStart = Math.Max(1, maxPage - totalSpan + 1);
                }

                var range = Enumerable.Range(currStart, totalSpan)
                    .Where(x => x <= TotalPageCount)
                    .ToList();

                return range;
            }
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPageCount;

        public bool TotalPageCountExceedSpan => PageIndex.GetValueOrDefault() + VisiblePagePadding < TotalPageCount;
    }
}
