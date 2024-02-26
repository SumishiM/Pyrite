using System;
using System.Collections.Generic;
using System.Text;

namespace Pyrite.Generator.Templating
{
    public static class Templates
    {
        public const string GeneratedCodeNamespace = "Pyrite.Generated";
        public const string SystemNamespace = "<system_namespace>";
        public const string ProjectNameToken = "<project_name>";
        public const string PercistantSystemsListToken = "<percistant_systems_list>";

        public const string PercistantSystemsImplementationRaw =
            $$"""

            """;
    }
}
