using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using NittyGritty.SourceGenerators.Receivers;

namespace NittyGritty.SourceGenerators
{
    [Generator]
    internal class CommandGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CommandContextReceiver());
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
            if (!(context.SyntaxContextReceiver is CommandContextReceiver receiver))
            {
                return;
            }

            var methodGroups = receiver.Methods.GroupBy(m => new { Name = m.Method.ContainingType?.Name, Namespace = m.Method.ContainingNamespace?.ToDisplayString() });
            foreach (var methodGroup in methodGroups)
            {
                var fileName = $"{methodGroup.Key.Namespace}.{methodGroup.Key.Name}.Commands.g.cs";
                var codeWriter = new CodeWriter();
                codeWriter.AppendLine("using System;");
                codeWriter.AppendLine("using NittyGritty.Commands;");
                codeWriter.AppendLine();
                using (codeWriter.BeginScope($"namespace {methodGroup.Key.Namespace}"))
                {
                    using (codeWriter.BeginScope($"public partial class {methodGroup.Key.Name}"))
                    {
                        foreach (var method in methodGroup)
                        {
                            if (method.Method.Parameters.Length > 1)
                            {
                                context.ReportDiagnostic(Diagnostic.Create(
                                    new DiagnosticDescriptor("NGCMD0001", "Method has more than 1 parameter", "Method '{0}' has {1} parameters. Method must not have more than 1 parameter", "NittyGritty", DiagnosticSeverity.Error, true),
                                    method.Method.Locations.FirstOrDefault(), method.Method.Name, method.Method.Parameters.Length));
                                continue;
                            }

                            if (!method.Method.ReturnsVoid && !method.Method.ReturnType.ToDisplayString().Equals("System.Threading.Tasks.Task"))
                            {
                                context.ReportDiagnostic(Diagnostic.Create(
                                    new DiagnosticDescriptor("NGCMD0002", "Invalid method return type", "Method '{0}' returns '{1}'. Method must return void or Task only.", "NittyGritty", DiagnosticSeverity.Error, true),
                                    method.Method.Locations.FirstOrDefault(), method.Method.Name, method.Method.ReturnType.ToDisplayString()));
                                continue;
                            }

                            GenerateCommand(codeWriter, method);
                        }
                    }
                }
                context.AddSource(fileName, codeWriter.ToString());
            }
        }

        private void GenerateCommand(CodeWriter codeWriter, MethodInfo methodInfo)
        {
            var isAsync = methodInfo.Method.ReturnType.Name switch
            {
                "Task" => true,
                _ => false
            };

            var commandType = isAsync ?
                "AsyncRelayCommand" :
                "RelayCommand";

            var parameter = methodInfo.Method.Parameters.FirstOrDefault();
            var hasParameter = parameter != null;
            var isParameterValueType = parameter != null && parameter.Type.IsValueType;
            var isParameterNullable = parameter != null && parameter.Type.NullableAnnotation == NullableAnnotation.Annotated;
            var commandParameterType = hasParameter ?
                $"<{parameter.Type.ToDisplayString()}{(isParameterValueType && !isParameterNullable ? "?" : string.Empty)}>" :
                string.Empty;
            var parameterName = hasParameter ? methodInfo.Method.Parameters[0].Name : string.Empty;

            var commandName = methodInfo.Attribute.GetValue<string>("Name") ?? methodInfo.Method.Name;

            codeWriter.AppendLine();
            codeWriter.AppendLine($"private {commandType}{commandParameterType} _{commandName};");
            codeWriter.AppendLine($"public {commandType}{commandParameterType} {commandName}Command => _{commandName} ?? (_{commandName} = new {commandType}{commandParameterType}(");
            codeWriter.AddIndent();
            using (codeWriter.BeginScope($"{(isAsync ? "async " : "")}({parameterName}) =>"))
            {
                if (isParameterValueType && !isParameterNullable)
                {
                    using (codeWriter.BeginScope($"if ({parameterName}.HasValue)"))
                    {
                        codeWriter.AppendLine($"{(isAsync ? "await " : "")}{methodInfo.Method.Name}({parameterName}.Value);");
                    }
                }
                else
                {
                    codeWriter.AppendLine($"{(isAsync ? "await " : "")}{methodInfo.Method.Name}({parameterName});");
                }
            }

            codeWriter.AppendLine("));");
            codeWriter.RemoveIndent();
            codeWriter.AppendLine();
        }

    }

}
