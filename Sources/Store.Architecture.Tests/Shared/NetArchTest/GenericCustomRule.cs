using Mono.Cecil;

namespace Store.Architecture.Tests;

internal class GenericCustomRule(Func<TypeDefinition, bool> typeCheck) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => typeCheck(type);
}