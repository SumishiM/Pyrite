using System;
using System.Collections.Generic;
using System.Text;

namespace Pyrite.Generator.Templating
{
    public static class Templates
    {
        public const string ProjectNameToken = "<project_name>";
        public const string ProjectGameClassAsPartialToken = "<project_game_class_as_partial>";
        public const string PercistentSystemsListToken = "<percistent_systems_list>";

        public const string PercistentSystemsRegisteringRaw =
            $$"""
            namespace {{ProjectNameToken}};

            {{ProjectGameClassAsPartialToken}}
            {
                // Registering every systems type tagged with the PercistentSystemAttribute
                protected override List<Type> PercistentSystems => [
                {{PercistentSystemsListToken}}    ];
            }
            """;
    }
}
