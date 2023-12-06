using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Utils
{
    public static class FileHelper
    {
        internal static readonly JsonSerializerSettings JsonSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ContractResolver = new WritablePropertiesOnlyResolver(),
            MissingMemberHandling = MissingMemberHandling.Error,
            Error = SerializationHelper.HandleSerializationError,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string EscapePath(this string path)
        {
            return path
                .Replace('\\', Path.DirectorySeparatorChar)
                .Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
