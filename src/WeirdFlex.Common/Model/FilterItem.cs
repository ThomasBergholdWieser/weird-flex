using System.Collections.Generic;
using System.Reflection;

namespace WeirdFlex.Common.Model
{
    public sealed class FilterItem
    {
        public string MemberName { get; }

        public object Value { get; }

        public FilterOperation Direction { get; set; } = FilterOperation.Equals;

        public FilterItem(string memberName, object value)
        {
            MemberName = memberName;
            Value = value;
        }

        public static ICollection<FilterItem>? FromModel<T>(T? model)
            where T : class
        {
            if (model == null)
            {
                return null;
            }

            var type = typeof(T);
            var props = type.GetProperties(
                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            List<FilterItem>? list = null;
            foreach (var prop in props)
            {
                var val = prop.GetValue(model);
                if (val == null)
                {
                    continue;
                }
                list ??= new List<FilterItem>();
                list.Add(new FilterItem(prop.Name, val));
            }

            return list;
        }
    }
}
