using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using NittyGritty.SourceGenerators.Attributes;
using NittyGritty.SourceGenerators.Receivers;

namespace NittyGritty.SourceGenerators
{
    [Generator]
    internal class DialogKeyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new DialogKeyContextReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached &&
                context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.NG_DevelopmentMode", out var dm) &&
                bool.TryParse(dm, out var developmentMode) &&
                developmentMode)
            {
                System.Diagnostics.Debugger.Launch();
            }
#endif

            if (!(context.SyntaxContextReceiver is DialogKeyContextReceiver receiver))
            {
                return;
            }

            var fileName = "DialogKeys.g.cs";
            var codeWriter = new CodeWriter();
            codeWriter.AppendLine("using System;");
            codeWriter.AppendLine();

            var dNamespace = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.NG_DialogKeysNamespace", out var ns) && !string.IsNullOrWhiteSpace(ns) ?
                ns : "NittyGritty.Generated";
            using (codeWriter.BeginScope($"namespace {dNamespace}"))
            {
                using (codeWriter.BeginScope($"public static class DialogKeys"))
                {
                    foreach (var item in receiver.Classes)
                    {
                        var dKeyAttribute = item.GetAttribute<DialogKeyAttribute>();
                        if (dKeyAttribute != null)
                        {
                            var key = dKeyAttribute.GetValue<string>("Key") ?? GetKey(item.Name);
                            codeWriter.AppendLine($"public static string {key} {{ get; }} = nameof({key});");
                        }
                    }
                }
            }

            context.AddSource(fileName, codeWriter.ToString());
        }

        private string GetKey(string name)
        {
            if (name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase))
            {
                return name.Substring(0, name.Length - 9);
            }
            else if (name.EndsWith("Dialog", StringComparison.OrdinalIgnoreCase))
            {
                return name.Substring(0, name.Length - 6);
            }
            return name;
        }

    }
}
