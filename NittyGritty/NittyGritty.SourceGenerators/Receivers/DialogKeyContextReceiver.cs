using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NittyGritty.SourceGenerators.Receivers
{
    internal class DialogKeyContextReceiver : ISyntaxContextReceiver
    {
        public List<INamedTypeSymbol> Classes { get; } = new List<INamedTypeSymbol>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax vm)
            {
                if (context.SemanticModel.GetDeclaredSymbol(vm) is INamedTypeSymbol vmSymbol &&
                    vmSymbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == "NittyGritty.SourceGenerators.Attributes.DialogKeyAttribute"))
                {
                    Classes.Add(vmSymbol);
                }
            }
        }
    }
}
