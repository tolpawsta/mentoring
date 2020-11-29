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
    class ClassDeclaredInEntutiesNamespaceShoudHavePublicPropertiesIdAndName : DiagnosticAnalyzer
    {
        public const string DiagnosticId = RuleIdentifiers.ClassFromEntitiesNamespacePublic;
        private static LocalizableString Title = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPropertiesTitle), Resources.ResourceManager, typeof(Resources));
        private static LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPropertiesMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static LocalizableString Description = new LocalizableResourceString(nameof(Resources.ClassFromEntitiesPropertiesDescription), Resources.ResourceManager, typeof(Resources));

        public static string Category = RuleCategories.Using;

        public static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
        {
            var typeSymbol = (INamedTypeSymbol)context.ContainingSymbol;
            var typeNamespace = typeSymbol.ContainingNamespace.Name;
            if (typeNamespace.Equals("Entities"))
            {
                var node = (ClassDeclarationSyntax)context.Node;
                var propertyId = node.GetPropertyDeclaration("Id");
                var propertyName = node.GetPropertyDeclaration("Name");
                var propertyId = node.Members.OfType<PropertyDeclarationSyntax>().FirstOrDefault(p => p.Identifier.Text.Equals("Id"));
                var propertyName = node.Members.OfType<PropertyDeclarationSyntax>().FirstOrDefault(p => p.Identifier.Text.Equals("Name"));
                if (!propertyId?.Modifiers.Any(SyntaxKind.PublicKeyword) ?? false || true)
                {
                    var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), node.Identifier.Text, propertyId?.Identifier.Text ?? "Id");
                    context.ReportDiagnostic(diagnostic);
                }
                if (!propertyName?.Modifiers.Any(SyntaxKind.PublicKeyword) ?? false || true)
                {
                    var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), node.Identifier.Text, propertyName?.Identifier.Text ?? "Name");
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
