using System.Linq;
using System.Reflection;

namespace WeirdFlex.Api
{
    public static class ApiVersion
    {
        public static string InformationalVersion => Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "n/a";

        public static string DisplayVersion => InformationalVersion.Split("+").First();
    }
}
