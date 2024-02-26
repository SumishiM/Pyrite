using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pyrite.Generator.Extentions;
using Pyrite.Generator.Metadata;
using Pyrite.Generator.Templating;
using System.Collections.Immutable;

namespace Pyrite.Generator
{
    [Generator]
    public class PyriteExtentionsGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var potentialPercistantSystems = context.PotentialPercistantSystems().Collect();

            var compilation = context.CompilationProvider.Combine(potentialPercistantSystems);

            context.RegisterSourceOutput(compilation,
                (context, source) => Execute(context, source.Left, source.Right));
        }

        private void Execute(
            SourceProductionContext context,
            Compilation compilation,
            ImmutableArray<TypeDeclarationSyntax> potentialPercistantSystems)
        {

            var pyriteTypesSymbols = PyriteTypesSymbols.FromCompilation(compilation);
            if (pyriteTypesSymbols is null)
                return;

            //var assemblyTypeFetcher = new AssemblyTypeFetcher(compilation);
            var metadataFetcher = new MetadataFetcher(compilation);

            var projectName = compilation.AssemblyName?.Replace(".", "") ?? "My";

            var projectMetadata = new TypeMetadata.Project(
                projectName
            );



            // create files as raw strings for now
            var templates = ImmutableArray.Create(
                FileTemplate.PercistantSystemsImplementation(projectName)
            );

            // replace project tokens in files
            foreach (var template in templates)
            {
                template.Process(projectMetadata);
            }

            var allTypeMetadata = metadataFetcher.Fetch(pyriteTypesSymbols, potentialPercistantSystems);

            // replace tokens in files
            foreach (var template in templates)
            {
                foreach (var metadata in allTypeMetadata)
                {
                    template.Process(metadata);
                }
            }

            // Create sources
            foreach (var template in templates)
            {
                context.AddSource(template.FileName, template.GetDocumentWithReplacements());
            }
        }
    }
}
