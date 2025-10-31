using Microsoft.AspNetCore.Mvc;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Architecture.Tests;

internal sealed class PublicMethodsUseTypesFromRule(string @namespace) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
        => type.GetMethods()
            .Where(method => method.IsPublic == true)
            .Any(method => method.FullName.Contains(@namespace));
}


internal static partial class CustomRules
{
    public static ConditionList PublicMethodsUseTypesFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new PublicMethodsUseTypesFromRule(@namespace));
}