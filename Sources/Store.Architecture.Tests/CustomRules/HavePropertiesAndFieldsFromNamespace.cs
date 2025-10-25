using Mono.Cecil;

namespace Store.Architecture.Tests;

internal sealed class HavePropertiesAndFieldsFromNamespace(string @namespace) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
        => type.Properties.Any(prop => prop.PropertyType.Namespace.Contains(@namespace)) ||
           type.Fields.Any(field => field.DeclaringType.Namespace.Contains(@namespace));

}