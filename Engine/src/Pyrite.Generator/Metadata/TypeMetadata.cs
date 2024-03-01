using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Pyrite.Generator.Metadata
{
    public abstract record TypeMetadata
    {
        public sealed record Project(
            string ProjectName,
            string ProjectGameClassName
            ) : TypeMetadata;

        public sealed record System(
            bool IsInternal,
            TypeKind Kind,
            string Name, 
            string FullName,
            string Namespace) : TypeMetadata;

    }
}
