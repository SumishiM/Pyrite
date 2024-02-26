using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pyrite.Generator.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Pyrite.Generator.Metadata
{
    public sealed class MetadataFetcher
    {
        private readonly Compilation _compilation;

        public MetadataFetcher(Compilation compilation)
        {
            _compilation = compilation;
        }

        /// <summary>
        /// Fetch every data we want for the generated code. Such as components, and create the metadata related to it.
        /// </summary>
        public IEnumerable<TypeMetadata> Fetch(
            PyriteTypesSymbols pyriteTypesSymbols,
            ImmutableArray<TypeDeclarationSyntax> potentialComponents)
        {
            var allValueType = potentialComponents
                .SelectMany(ValueTypeFromTypeDeclarationSyntax)
                .ToImmutableArray();

            var components = FetchSystems(pyriteTypesSymbols, allValueType);
            foreach (var component in components)
            {
                yield return component;
            }
        }


        /// <summary>
        /// Fetch components data and create the metadata for it
        /// </summary>
        private IEnumerable<TypeMetadata.System> FetchSystems(
            PyriteTypesSymbols pyriteTypesSymbols,
            ImmutableArray<INamedTypeSymbol> allValueTypes)
            => allValueTypes
                .Where(t =>
                    (!t.IsGenericType || !t.IsAbstract)
                    && t.ImplementInterface(pyriteTypesSymbols.SystemTypeSymbol)
                    && t.HasAttribute(pyriteTypesSymbols.PercistantSystemAttribute))
                .OrderBy(c => c.Name)
                .Select((system, index) => new TypeMetadata.System(
                    Name: system.Name.ToCleanAttributeName(),
                    FullName: system.FullName(),
                    Namespace: system.ContainingNamespace.Name,
                    Kind: system.TypeKind,
                    IsInternal: system.DeclaredAccessibility == Accessibility.Internal));

        /// <summary>
        /// Get every value types for syntax we are interessed in
        /// </summary>
        private IEnumerable<INamedTypeSymbol> ValueTypeFromTypeDeclarationSyntax(
            TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var sementic = _compilation.GetSemanticModel(typeDeclarationSyntax.SyntaxTree);
            if (sementic.GetDeclaredSymbol(typeDeclarationSyntax) is not INamedTypeSymbol potentialPercistantSystemSymbol)
                return Enumerable.Empty<INamedTypeSymbol>();

            if (potentialPercistantSystemSymbol.GetAttributes().Count() == 0)
                return Enumerable.Empty<INamedTypeSymbol>();

            if (typeDeclarationSyntax is RecordDeclarationSyntax && !potentialPercistantSystemSymbol.IsValueType)
                return Enumerable.Empty<INamedTypeSymbol>();

            return potentialPercistantSystemSymbol.Yield();
        }
    }
}
