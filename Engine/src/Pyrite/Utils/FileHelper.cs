using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        // content directory
#if !CONSOLE
        private static string? AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
#endif
        /*
        public static string ContentDirectory
        {
#if PS4
            get { return Path.Combine("/app0/", Game.Instance.Content.RootDirectory); }
#elif NSWITCH
            get { return Path.Combine("rom:/", Game.Instance.Content.RootDirectory); }
#elif XBOXONE
            get { return Game.Instance.Content.RootDirectory; }
#else
            get { return Path.Combine(AssemblyDirectory!, Game.Instance.Content.RootDirectory); }
#endif
        }
        */
    }
}
