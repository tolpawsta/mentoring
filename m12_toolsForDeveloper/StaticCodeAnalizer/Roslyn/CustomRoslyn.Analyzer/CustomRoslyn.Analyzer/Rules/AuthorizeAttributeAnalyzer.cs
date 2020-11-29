using CustomRoslyn.Analyzer.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace CustomRoslyn.Analyzer.Rules
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AuthorizeAttributeAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = RuleIdentifiers.ControllerAuthorizeAttribute;

        private static LocalizableString Title = new LocalizableResourceString(
                                                     nameof(Resources.AuthorizeAttributeAnalyzerTitle),
                                                     Resources.ResourceManager,
                                                     typeof(Resources));
        private static LocalizableString MessageFormat = new LocalizableResourceString(
                                                     nameof(Resources.AuthorizeAttributeAnalyzerMessageFormat),
                                                     Resources.ResourceManager,
                                                     typeof(Resources));
        private static LocalizableString Description = new LocalizableResourceString(
                                                     nameof(Resources.AuthorizeAttributeAnalyzerDescription),
                                                     Resources.ResourceManager,
                                                     typeof(Resources));
        public static string Category = RuleCategories.Using;

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
                                                     DiagnosticId,
                                                     Title,
                                                     MessageFormat,
                                                     Category,
                                                     DiagnosticSeverity.Warning,
                                                     true,
                                                     description: Description);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalizeSyntaxNode, SyntaxKind.ClassDeclaration);
        }

        private void AnalizeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var classNode = (ClassDeclarationSyntax)context.Node;            
            var hasClassAuthorizeAttribute = classNode.HasSyntaxNodeAttribute("Authorize");
            var classSymbol = (INamedTypeSymbol) context.ContainingSymbol;
            var baseType = classSymbol.BaseType;
            var isBaseTypeContoller = baseType.Name.ToString().Equals("Controller") && baseType.IsSymbolNamespaceEqual("System.Web.Mvc");            
            if (!hasClassAuthorizeAttribute && isBaseTypeContoller)
            {
                var publicmethods = classNode.Members.Cast<MethodDeclarationSyntax>().Where(method => method.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)));
                var haspublicmethodsauthorizeattribute = publicmethods.All(method => method.HasSyntaxNodeAttribute("Authorize"));
                if (!haspublicmethodsauthorizeattribute)
                {
                    var diagnostic = Diagnostic.Create(Rule, classNode.Identifier.GetLocation(), classNode.Identifier.Text);
                    context.ReportDiagnostic(diagnostic);
                }
            }

        }
    }
}
