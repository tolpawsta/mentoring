using StyleCop.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleCop.CustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerPostfixRule: SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            var csharpDocument = document as CsDocument;
            if (csharpDocument != null)
            {
                csharpDocument.WalkDocument(
                    new CodeWalkerElementVisitor<ControllerPostfixRule>(this.VisitElement),
                    this);
            }
        }

        private bool VisitElement(CsElement element, CsElement parentElement, ControllerPostfixRule context)
        {
            if (element.ElementType == ElementType.Method)
            {
                int firstLineNumber = element.LineNumber;
                int lastLineNumer = firstLineNumber;

                foreach (var statement in element.ChildStatements)
                {
                    lastLineNumer = statement.LineNumber;
                }

                int numberOfLinesInMethod = lastLineNumer - firstLineNumber + 1;
                if (numberOfLinesInMethod > 2)
                {
                    context.AddViolation(element, "TooLongMethod", element.Declaration.Name,
                        numberOfLinesInMethod, 2);
                }
            }
            return true;
        }
    }
}
