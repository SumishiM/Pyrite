﻿using Pyrite.Generator.Metadata;
using System.Collections.Immutable;

using static Pyrite.Generator.Templating.TemplateSubstitution;

namespace Pyrite.Generator.Templating
{
    public class FileTemplate
    {
        public string FileName;
        private readonly string _templateText;
        private readonly ImmutableArray<TemplateSubstitution> _substitutions;

        public FileTemplate(
            string fileName,
            string templateText,
            ImmutableArray<TemplateSubstitution> substitutions)
        {
            FileName = fileName;
            _templateText = templateText;
            _substitutions = substitutions;
        }

        /// <summary>
        /// Component lookup table generated file
        /// </summary>
        public static FileTemplate PercistantSystemsRegistration(string projectName)
            => new($"{projectName}PercistantSystemsRegistration.g.cs",
                Templates.PercistantSystemsRegisteringRaw,
                ImmutableArray.Create<TemplateSubstitution>(
                    new ProjectNameSubstitution(),
                    new ProjectGameClassAsPartialSubstitution(),
                    new PercistantSystemsListSubstitution()));
    
        public void Process(TypeMetadata metadata)
        {
            foreach (var substitutions in _substitutions)
            {
                substitutions.Process(metadata);
            }
        }

        public string GetDocumentWithReplacements()
            => _substitutions.Aggregate(
                _templateText,
                (text, substitution) => text.Replace(
                    substitution.TemplateToReplace,
                    substitution.GetProcessedText()));
    }
}
