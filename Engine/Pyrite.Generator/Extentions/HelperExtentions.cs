using Microsoft.CodeAnalysis;

namespace Pyrite.Generator.Extentions
{
    public static class HelperExtentions
    {
        public static bool HasAttribute(this INamedTypeSymbol symbol, INamedTypeSymbol attributeToCheck)
            => symbol.GetAttributes().Any(attributeSymbol => SymbolEqualityComparer.Default.Equals(attributeSymbol.AttributeClass, attributeToCheck));

        public static bool ImplementInterface(this INamedTypeSymbol symbol, INamedTypeSymbol interfaceToCheck)
            => symbol.AllInterfaces
                .Any(interfaceSymbol => SymbolEqualityComparer.Default.Equals(interfaceSymbol, interfaceToCheck));

        public static bool IsSubclassOf(this INamedTypeSymbol symbol, INamedTypeSymbol typeToCheck)
        {
            ITypeSymbol? nextTypeToVerify = symbol;
            do
            {
                var subtype = nextTypeToVerify?.BaseType;
                if (subtype is not null && SymbolEqualityComparer.Default.Equals(subtype, typeToCheck))
                {
                    return true;
                }

                nextTypeToVerify = subtype;

            } while (nextTypeToVerify is not null);

            return false;
        }

        public static IEnumerable<T> Yield<T>(this T obj)
        {
            yield return obj;
        }

        private static readonly SymbolDisplayFormat _fullNameDisplayFormat =
            new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        public static string FullName(this ITypeSymbol symbol)
        {
            var fullTypeName = symbol.ToDisplayString(_fullNameDisplayFormat);

            if (fullTypeName.Contains("?") || symbol is not INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
                return fullTypeName;

            var genericTypes = string.Join(", ",
                namedTypeSymbol.TypeArguments.Select(x => $"global::{x.FullName()}"));

            return $"{fullTypeName}<{genericTypes}>";
        }

        private const string Component = "Component";
        private const string Attribute = "Attribute";

        public static string ToCleanComponentName(this string value)
            => value.EndsWith(Component) ? value[..^Component.Length] : value;
        public static string ToCleanAttributeName(this string value)
            => value.EndsWith(Attribute) ? value[..^Attribute.Length] : value;
    }
}
