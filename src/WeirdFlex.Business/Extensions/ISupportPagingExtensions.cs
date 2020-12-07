using System.Linq;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Common.Model;

namespace Tieto.Lama.Business.UseCases
{
    public static class ISupportPagingExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this ISupportPaging pagingProvider, IQueryable<T> query)
        {
            if (pagingProvider.Pagination == null)
            {
                return query;
            }

            var pagination = pagingProvider.Pagination;

            if (pagination.PageSize.HasValue && 
                pagination.PageIndex.HasValue)
            {
                var skip = (pagination.PageIndex.Value - 1) * pagination.PageSize.Value;
                var take = pagination.PageSize.Value;

                query = query
                    .Skip(skip)
                    .Take(take);
            }

            return query;
        }

        public static PageInfo? BuildPage(this ISupportPaging pagingProvider, int? totalRowCount = null)
        {
            if(pagingProvider.Pagination == null)
            {
                return null;
            }

            var pagination = pagingProvider.Pagination;

            return new PageInfo(pagination.PageIndex, totalRowCount) { PageSize = pagination.PageSize };
        }
    }
}
