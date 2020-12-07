using WeirdFlex.Common.Model;

namespace WeirdFlex.Business.Interfaces
{
    public interface IPaginatedResult
    {
        PageInfo? PageInfo { get; }
    }
}
