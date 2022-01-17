using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using NittyGritty.SourceGenerators.Attributes;
using NittyGritty.SourceGenerators.Receivers;

namespace NittyGritty.SourceGenerators
{
    [Generator]
    internal class ViewModelKeyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ViewModelKeyContextReceiver());
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
            if (!(context.SyntaxContextReceiver is ViewModelKeyContextReceiver receiver))
            {
                return;
            }

            var fileName = "ViewModelKeys.g.cs";
            var codeWriter = new CodeWriter();
            codeWriter.AppendLine("using System;");
            codeWriter.AppendLine();

            var vmNamespace = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.NG_ViewModelKeysNamespace", out var ns) && !string.IsNullOrWhiteSpace(ns) ?
                ns : "NittyGritty.Generated";
            using (codeWriter.BeginScope($"namespace {vmNamespace}"))
            {
                using (codeWriter.BeginScope($"public static class ViewModelKeys"))
                {
                    foreach (var item in receiver.Classes)
                    {
                        var vmKeyAttribute = item.GetAttribute<ViewModelKeyAttribute>();
                        if (vmKeyAttribute != null)
                        {
                            var key = vmKeyAttribute.GetValue<string>("Key") ?? GetKey(item.Name);
                            codeWriter.AppendLine($"public static string {key} {{ get; }} = nameof({key});");
                        }
                    }
                }
            }

            context.AddSource(fileName, codeWriter.ToString());
        }

        private string GetKey(string name)
        {
            return name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase) ?
                name.Substring(0, name.Length - 9) :
                name;
        }

    }
}
