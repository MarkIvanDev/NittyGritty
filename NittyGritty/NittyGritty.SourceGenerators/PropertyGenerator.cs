using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using NittyGritty.SourceGenerators.Attributes;
using NittyGritty.SourceGenerators.Receivers;

namespace NittyGritty.SourceGenerators
{
    [Generator]
    internal class PropertyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new PropertyContextReceiver());
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
            if (!(context.SyntaxContextReceiver is PropertyContextReceiver receiver))
            {
                return;
            }

            var fieldGroups = receiver.Fields.GroupBy(f => new { Type = f.ContainingType, Namespace = f.ContainingNamespace?.ToDisplayString() });
            foreach (var fieldGroup in fieldGroups)
            {
                var baseType = fieldGroup.Key.Type?.BaseType;
                if (baseType != null &&
                    baseType.IsDerivedFromType("NittyGritty.ObservableObject"))
                {
                    var fileName = $"{fieldGroup.Key.Namespace}.{fieldGroup.Key.Type.Name}.Properties.g.cs";
                    var codeWriter = new CodeWriter();
                    codeWriter.AppendLine("using System;");
                    codeWriter.AppendLine();
                    using (codeWriter.BeginScope($"namespace {fieldGroup.Key.Namespace}"))
                    {
                        using (codeWriter.BeginScope($"public partial class {fieldGroup.Key.Type.Name}"))
                        {
                            foreach (var field in fieldGroup)
                            {
                                var notifyAttribute = field.GetAttribute<NotifyAttribute>();
                                if (notifyAttribute is null)
                                {
                                    context.ReportDiagnostic(Diagnostic.Create(
                                        new DiagnosticDescriptor("NGP0002", "Field should be marked with NotifyAttribute", "Field '{0}' is not marked with NotifyAttribute.", "NittyGritty", DiagnosticSeverity.Error, true),
                                        field.Locations.FirstOrDefault(), field.Name));
                                    continue;
                                }

                                if (field.IsReadOnly)
                                {
                                    context.ReportDiagnostic(Diagnostic.Create(
                                        new DiagnosticDescriptor("NGP0003", "Field should not be readonly", "Field '{0}' is readonly.", "NittyGritty", DiagnosticSeverity.Error, true),
                                        field.Locations.FirstOrDefault(), field.Name));
                                    continue;
                                }

                                GenerateProperty(codeWriter, field, notifyAttribute);
                                codeWriter.AppendLine();
                            }
                        }
                    }
                    context.AddSource(fileName, codeWriter.ToString());
                }
                else
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor("NGP0001", "Containing Type should extend ObservableObject", "Class '{0}' does not derive from ObservableObject.", "NittyGritty", DiagnosticSeverity.Error, true),
                        fieldGroup.Key.Type?.Locations.FirstOrDefault(), fieldGroup.Key.Type?.Name));
                    continue;
                }
            }
        }

        private void GenerateProperty(CodeWriter codeWriter, IFieldSymbol field, AttributeData notifyAttribute)
        {
            var propertyName = notifyAttribute.GetValue<string>("Name") ?? GetName(field.Name);
            var (propertyAccess, getterAccess, setterAccess) = GetAccessLevels(notifyAttribute.GetValue<AccessLevel>("Getter"), notifyAttribute.GetValue<AccessLevel>("Setter"));
            var alsoNotifyAttributes = field.GetAttributes<AlsoNotifyAttribute>();

            using (codeWriter.BeginScope($"{propertyAccess}{field.Type.ToDisplayString()} {propertyName}"))
            {
                codeWriter.AppendLine($@"{getterAccess}get {{ return {field.Name}; }}");

                using (codeWriter.BeginScope($"{setterAccess}set"))
                {
                    codeWriter.AppendLine($"Set(ref {field.Name}, value);");
                    foreach (var item in alsoNotifyAttributes)
                    {
                        codeWriter.AppendLine($"RaisePropertyChanged(\"{item.GetValue<string>("Name")}\");");
                    }
                }
            }
        }

        private string GetName(string name)
        {
            var newName = name.TrimStart('_');
            var firstLetter = newName[0];
            return $"{char.ToUpper(firstLetter)}{newName.Substring(1)}";
        }

        private (string propertyAccess, string getterAccess, string setterAccess) GetAccessLevels(AccessLevel getter, AccessLevel setter)
        {
            if (getter == AccessLevel.Internal && setter == AccessLevel.Protected ||
                getter == AccessLevel.Protected && setter == AccessLevel.Internal)
            {
                return ("protected internal ", string.Empty, string.Empty);
            }
            else if (getter == setter)
            {
                return (getter == AccessLevel.Public ? "public " : getter.GetDescription(), string.Empty, string.Empty);
            }
            else if (getter > setter)
            {
                return (setter == AccessLevel.Public ? "public " : setter.GetDescription(), getter.GetDescription(), string.Empty);
            }
            else if (setter > getter)
            {
                return (getter == AccessLevel.Public ? "public " : getter.GetDescription(), string.Empty, setter.GetDescription());
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

}
