using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WeirdFlex.Data.EF
{
    public static class DbFunctionsExpressions
    {
        public static readonly MethodInfo LikeMethod = typeof(DbFunctionsExtensions)
                    .GetMethod("Like", new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;
    }
}
