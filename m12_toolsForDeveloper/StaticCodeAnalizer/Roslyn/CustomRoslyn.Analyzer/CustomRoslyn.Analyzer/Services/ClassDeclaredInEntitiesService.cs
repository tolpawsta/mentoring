using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomRoslyn.Analyzer.Services
{
    public static class ClassDeclaredInEntitiesService
    {
        public static PropertyDeclarationSyntax GetPropertyDeclaration (this TypeDeclarationSyntax typeDeclaration, string propertyName)
        {
            return typeDeclaration.Members.OfType<PropertyDeclarationSyntax>().FirstOrDefault(t=>t.Identifier.Text.Equals(propertyName));
        }

            }
}
