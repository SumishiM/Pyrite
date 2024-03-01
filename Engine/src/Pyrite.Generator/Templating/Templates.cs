using System;
using System.Collections.Generic;
using System.Text;

namespace Pyrite.Generator.Templating
{
    public static class Templates
    {
        public const string ProjectNameToken = "<project_name>";
        public const string ProjectGameClassAsPartialToken = "<project_game_class_as_partial>";
        public const string PercistantSystemsListToken = "<percistant_systems_list>";

        public const string PercistantSystemsRegisteringRaw =
            $$"""
            namespace {{ProjectNameToken}};

            {{ProjectGameClassAsPartialToken}}
            {
                // Registering every systems type tagged with the PercistantSystemAttribute
                protected override List<Type> PercistantSystems => [
                {{PercistantSystemsListToken}}    ];
            }
            """;
    }
}
