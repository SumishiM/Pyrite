using Microsoft.CodeAnalysis;
using Pyrite.Generator.Metadata;

namespace Pyrite.Generator.Templating
{
    public abstract partial class TemplateSubstitution
    {
        internal sealed class ProjectNameSubstitution : TemplateSubstitution
        {
            public ProjectNameSubstitution() : base(Templates.ProjectNameToken) { }

            protected override string? ProcessProject(TypeMetadata.Project project)
                => project.ProjectName;
        }

        internal sealed class ProjectGameClassAsPartialSubstitution : TemplateSubstitution
        {
            public ProjectGameClassAsPartialSubstitution() : base(Templates.ProjectGameClassAsPartialToken) { }

            protected override string? ProcessProject(TypeMetadata.Project project)
            {
                return
                    $$"""
                    public partial class {{project.ProjectGameClassName}}
                    """;
            }
        }

        internal sealed class PercistentSystemsListSubstitution : TemplateSubstitution
        {
            public PercistentSystemsListSubstitution() : base(Templates.PercistentSystemsListToken) { }


            protected override string? ProcessSystem(TypeMetadata.System system)
            {
                return
                    $$"""
                        typeof(global::{{system.FullName}}),

                    """;
            }
        }
    }
}
