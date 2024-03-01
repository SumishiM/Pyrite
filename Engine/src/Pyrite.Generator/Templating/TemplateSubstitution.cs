using Pyrite.Generator.Metadata;
using System.Text;

namespace Pyrite.Generator.Templating
{
    public abstract partial class TemplateSubstitution
    {
        private readonly StringBuilder _aggregatedText = new();

        public string TemplateToReplace { get; }
        protected string _projectName = "";
        protected string _parentProjectName = "";

        public TemplateSubstitution(string templateToReplace) 
        {
            TemplateToReplace = templateToReplace;
        }

        public void Process(TypeMetadata metadata)
        {
            var result = metadata switch
            {

                TypeMetadata.Project project => SaveAndProcessProject(project),
                TypeMetadata.System system => ProcessSystem(system),
                _ => throw new InvalidOperationException()
            };

            if (result is not null)
            {
                _aggregatedText.Append(result);
            }
        }

        private string? SaveAndProcessProject(TypeMetadata.Project project)
        {
            _projectName = project.ProjectName;
            return ProcessProject(project);
        }

        protected virtual string? ProcessProject(TypeMetadata.Project project) => null;
        protected virtual string? ProcessSystem(TypeMetadata.System system) => null;
        protected virtual string? FinalModification() => null;

        public string GetProcessedText()
        {
            var finalModification = FinalModification();
            if (finalModification is not null)
            {
                _aggregatedText.Append(finalModification);
            }

            return _aggregatedText.ToString();
        }
    }
}
    