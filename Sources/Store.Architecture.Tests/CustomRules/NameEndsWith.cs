using Mono.Cecil;

namespace Store.Architecture.Tests;

internal sealed class NameEndsWith(string suffix) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => type.Name.EndsWith(suffix);
}