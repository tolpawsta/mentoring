using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CustomRoslyn.Analyzer.Rules
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ClassDerivedFromControllerShoudHasSuffixControllerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = RuleIdentifiers.ControllerClassSuffix;

        private static LocalizableString Title = new LocalizableResourceString(
                                                     nameof(Resources.ControllerSuffixAnalyzerTitle),
                                                     Resources.ResourceManager,
                                                     typeof(Resources));
        private static LocalizableString MessageFormat = new LocalizableResourceString(
                                                             nameof(Resources.ControllerSuffixAnalyzerMessageFormat), 
                                                             Resources.ResourceManager, 
                                                             typeof(Resources));
        private static LocalizableString Description = new LocalizableResourceString(
                                                           nameof(Resources.ControllerSuffixAnalyzerDescription), 
                                                           Resources.ResourceManager, 
                                                           typeof(Resources));

        private const string Category = RuleCategories.Naming;

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

            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            var baseTypeName = namedTypeSymbol.BaseType.Name;

            if (baseTypeName.Equals("Controller") && !namedTypeSymbol.Name.EndsWith("Controller"))
            {
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
