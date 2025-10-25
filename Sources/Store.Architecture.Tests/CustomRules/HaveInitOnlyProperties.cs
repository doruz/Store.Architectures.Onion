using Mono.Cecil;

namespace Store.Architecture.Tests;

internal sealed class HaveInitOnlyProperties() : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => type.Fields.All(field => field.IsInitOnly);
}