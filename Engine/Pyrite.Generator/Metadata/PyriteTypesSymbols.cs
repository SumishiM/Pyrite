using Microsoft.CodeAnalysis;

namespace Pyrite.Generator.Metadata
{
    public sealed class PyriteTypesSymbols
    {
        private const string SystemTypeSymbolName = "Ignite.Systems.ISystem";
        private const string PercistantSystemAttributeTypeSymbolName = "Pyrite.Attributes.PercistantSystemAttribute";

        public INamedTypeSymbol SystemTypeSymbol;
        public INamedTypeSymbol PercistantSystemAttribute;

        private PyriteTypesSymbols(
            INamedTypeSymbol systemTypeSymbol,
            INamedTypeSymbol percistantSystemAttribute)
        {
            SystemTypeSymbol = systemTypeSymbol;
            PercistantSystemAttribute = percistantSystemAttribute;
        }

        public static PyriteTypesSymbols? FromCompilation(Compilation compilation)
        {
            var systemInterface = compilation.GetTypeByMetadataName(SystemTypeSymbolName);
            if (systemInterface is null)
                return null;

            var percistantSystemAttribute = compilation.GetTypeByMetadataName(PercistantSystemAttributeTypeSymbolName);
            if (percistantSystemAttribute is null)
                return null;

            return new(
                systemInterface,
                percistantSystemAttribute);
        }
    }
}
