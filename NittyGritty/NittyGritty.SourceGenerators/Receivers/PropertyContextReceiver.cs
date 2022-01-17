using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NittyGritty.SourceGenerators.Receivers
{
    internal class PropertyContextReceiver : ISyntaxContextReceiver
    {
        public List<IFieldSymbol> Fields { get; } = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
            if (context.Node is FieldDeclarationSyntax field)
            {
                foreach (var item in field.Declaration.Variables)
                {
                    if (context.SemanticModel.GetDeclaredSymbol(item) is IFieldSymbol fieldSymbol &&
                        fieldSymbol.GetAttributes().Any(a => a.AttributeClass?.ContainingNamespace?.ToDisplayString() == "NittyGritty.SourceGenerators.Attributes"))
                    {
                        Fields.Add(fieldSymbol);
                    }
                }
            }
        }
    }
}
