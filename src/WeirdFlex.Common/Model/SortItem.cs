using WeirdFlex.Common.Enums;

namespace WeirdFlex.Common.Model
{
    public sealed class SortItem
    {
        public string MemberName { get; }
        public SortDirection Direction { get; set; } = SortDirection.Ascending;
        public int? Order { get; set; }

        public SortItem(string memberName)
        {
            MemberName = memberName;
        }
    }
}
