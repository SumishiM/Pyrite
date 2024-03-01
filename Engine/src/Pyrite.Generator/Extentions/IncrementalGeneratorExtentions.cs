using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace Pyrite.Generator.Extentions
{
    public static class IncrementalGeneratorExtentions
    {
        /// <summary>
        /// Get every syntax that can potentially be a percistant system
        /// </summary>
        public static IncrementalValuesProvider<TypeDeclarationSyntax> PotentialPercistantSystems(
            this IncrementalGeneratorInitializationContext context)
            => context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (c, _) => c is TypeDeclarationSyntax,
                transform: (node, _) => (TypeDeclarationSyntax)node.Node)
                    .Where(c => c is not null);

    }
}
