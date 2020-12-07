using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
    public class LambdaEqualityComparer<TSource, TComparable> : IEqualityComparer<TSource>
    {
        readonly Func<TSource, TComparable> _keyGetter;

        public LambdaEqualityComparer(Func<TSource, TComparable> keyGetter)
        {
            _keyGetter = keyGetter;
        }

        public bool Equals([AllowNull] TSource x, [AllowNull] TSource y)
        {
            if (x == null || y == null) 
                return (x == null && y == null);

            return Equals(_keyGetter(x), _keyGetter(y));
        }

        public int GetHashCode([DisallowNull] TSource obj)
        {
            var k = _keyGetter(obj);
            if (k == null) return int.MaxValue;
            return k.GetHashCode();
        }
    }
}
