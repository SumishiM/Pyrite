using Microsoft.CodeAnalysis;

namespace Pyrite.Generator.Metadata
{
    public sealed class PyriteTypesSymbols
    {
        private const string GameSymbolName = "Pyrite.Game";
        private const string SystemTypeSymbolName = "Ignite.Systems.ISystem";
        private const string PercistantSystemAttributeTypeSymbolName = "Pyrite.Attributes.PercistantSystemAttribute";

        public INamedTypeSymbol GameTypeSymbol;
        public INamedTypeSymbol SystemTypeSymbol;
        public INamedTypeSymbol PercistantSystemAttribute;

        private PyriteTypesSymbols(
            INamedTypeSymbol gameTypeSymbol,
            INamedTypeSymbol systemTypeSymbol,
            INamedTypeSymbol percistantSystemAttribute)
        {
            GameTypeSymbol = gameTypeSymbol;
            SystemTypeSymbol = systemTypeSymbol;
            PercistantSystemAttribute = percistantSystemAttribute;
        }

        public static PyriteTypesSymbols? FromCompilation(Compilation compilation)
        {
            var gameClass = compilation.GetTypeByMetadataName(GameSymbolName);
            if (gameClass is null)
                return null;

            var systemInterface = compilation.GetTypeByMetadataName(SystemTypeSymbolName);
            if (systemInterface is null)
                return null;

            var percistantSystemAttribute = compilation.GetTypeByMetadataName(PercistantSystemAttributeTypeSymbolName);
            if (percistantSystemAttribute is null)
                return null;

            return new(
                gameClass,
                systemInterface,
                percistantSystemAttribute);
        }
    }
}
