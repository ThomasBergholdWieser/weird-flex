namespace WeirdFlex.Common.Model
{
    public sealed class Pagination
    {
        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public static Pagination FromModel(PageInfo? pageInfo = null, int? nextPageIndex = null, int? nextPageSize = null)
        {
            return new Pagination { PageIndex = nextPageIndex ?? pageInfo?.PageIndex, PageSize = nextPageSize ?? pageInfo?.PageSize };
        }
    }
}
