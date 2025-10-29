using Mono.Cecil;

namespace Store.Architecture.Tests;

internal sealed class MakeUseOfRule(string dependency) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        return type.Methods.Any(DoesMethodUseDomain);
    }

    private bool DoesMethodUseDomain(MethodDefinition method)
    {
        if (method.FullName.Contains(dependency))
        {
            return true;
        }

        return method.Body.Instructions
            .Select(instruction => instruction.ToString())
            .Any(instruction => instruction.Contains(dependency));
    }
}

internal static partial class CustomRules
{
    public static ConditionList HaveDependencyOn(this Conditions conditions, string dependency)
        => conditions.MeetCustomRule(new MakeUseOfRule(dependency));
}