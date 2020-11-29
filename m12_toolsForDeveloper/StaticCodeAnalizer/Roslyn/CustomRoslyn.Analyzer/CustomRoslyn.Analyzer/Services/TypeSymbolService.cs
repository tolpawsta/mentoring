using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRoslyn.Analyzer.Services
{
    public static class TypeSymbolService
    {
        public static bool IsSymbolNamespaceEqual(this ITypeSymbol symbol,string comparedString)
        {
            return symbol.ContainingNamespace.ToDisplayString().Equals(comparedString);
        }
    }
}
