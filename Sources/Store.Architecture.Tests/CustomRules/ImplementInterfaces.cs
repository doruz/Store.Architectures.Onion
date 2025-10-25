using Mono.Cecil;

namespace Store.Architecture.Tests;

internal sealed class ImplementInterfaces : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => type.HasInterfaces == true;
}