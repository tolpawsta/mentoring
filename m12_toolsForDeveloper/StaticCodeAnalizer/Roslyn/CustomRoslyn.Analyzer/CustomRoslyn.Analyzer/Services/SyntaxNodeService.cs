using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomRoslyn.Analyzer.Services
{
    public static class SyntaxNodeService
    {
        public static bool HasSyntaxNodeAttribute(this MemberDeclarationSyntax node, string attributeName)
        {          
            return node.AttributeLists.Any(al => al.Attributes.Any(a => a.Name.ToString().Equals(attributeName)));
        }
    }
}
