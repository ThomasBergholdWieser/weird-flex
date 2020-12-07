
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Linq
{
    public static class JObjectExtensions
    {
        public static Dictionary<string, string?> GetDataDictionary(this JObject? data)
        {
            if (data == null)
            {
                return new Dictionary<string, string?>();
            }

            return ((IDictionary<string, JToken?>)data)
                .ToDictionary(x => x.Key, x => x.Value!.Value<string?>());
        }
    }
}
