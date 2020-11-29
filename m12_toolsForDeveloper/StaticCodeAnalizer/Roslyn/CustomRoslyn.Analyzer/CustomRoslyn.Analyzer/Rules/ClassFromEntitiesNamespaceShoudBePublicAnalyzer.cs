using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CustomRoslyn.Analyzer.Rules
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ClassFromEntitiesNamespaceShoudBePublicAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = RuleIdentifiers.ClassFromEntitiesNamespacePublic;
        private static LocalizableString Title = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPublicTitle), Resources.ResourceManager, typeof(Resources));
        private static LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPublicMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static LocalizableString Description = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPublicDescription), Resources.ResourceManager, typeof(Resources));

        public static string Category = RuleCategories.Using;

        public static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId,Title,MessageFormat,Category,DiagnosticSeverity.Warning,true,Description);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
        {
            var typeSymbol =(INamedTypeSymbol) context.ContainingSymbol;
            var typeNamespace = typeSymbol.ContainingNamespace.Name;
            if (typeNamespace.Equals("Entities"))
            {
                var node = (ClassDeclarationSyntax)context.Node;
                var hasPublicModifier = node.Modifiers.Any(SyntaxKind.PublicKeyword);
                if (!hasPublicModifier)
                {
                    var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), node.Identifier.Text);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
