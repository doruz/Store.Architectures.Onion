using Mono.Cecil;

namespace Store.Architecture.Tests;

internal class CustomRule(Func<TypeDefinition, bool> typeCheck) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => typeCheck(type);

    public static implicit operator CustomRule(Func<TypeDefinition, bool> typeCheck) => new(typeCheck);
}