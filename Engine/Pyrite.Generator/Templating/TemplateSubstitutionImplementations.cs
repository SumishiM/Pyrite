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

        internal sealed class PercistantSystemsSubstitution : TemplateSubstitution
        {
            public PercistantSystemsSubstitution() : base(Templates.PercistantSystemsListToken) { }

            protected override string? ProcessSystem(TypeMetadata.System system)
            {
                string id = $"global::Ignite.{(string.IsNullOrEmpty(_parentProjectName) ? "Components" : "Generated")}.{_parentProjectName}ComponentLookupTable.{_parentProjectName}NextLookupId + {component.Index}";
                return $$"""
                namespace {{system.Namespace}}
                {
                    public partial {{system.Kind}} {{system.Name}}
                    {

                    }
                }

                """;
            }
        }
    }
}
