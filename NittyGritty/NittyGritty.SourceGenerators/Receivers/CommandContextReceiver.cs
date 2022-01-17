using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NittyGritty.SourceGenerators.Attributes;

namespace NittyGritty.SourceGenerators.Receivers
{
    internal class CommandContextReceiver : ISyntaxContextReceiver
    {
        public List<MethodInfo> Methods { get; } = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is MethodDeclarationSyntax method &&
                context.SemanticModel.GetDeclaredSymbol(method) is IMethodSymbol methodSymbol &&
                methodSymbol.GetAttribute<CommandAttribute>() is AttributeData attribute)
            {
                Methods.Add(new MethodInfo(methodSymbol, attribute));
            }
        }
    }

    internal class MethodInfo
    {
        public MethodInfo(IMethodSymbol method, AttributeData attribute)
        {
            Method = method;
            Attribute = attribute;
        }

        public IMethodSymbol Method { get; }

        public AttributeData Attribute { get; }
    }
}
