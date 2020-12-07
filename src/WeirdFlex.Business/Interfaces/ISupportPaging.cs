using WeirdFlex.Common.Model;

namespace WeirdFlex.Business.Interfaces
{
    public interface ISupportPaging
    {
        Pagination? Pagination { get; }
    }
}
