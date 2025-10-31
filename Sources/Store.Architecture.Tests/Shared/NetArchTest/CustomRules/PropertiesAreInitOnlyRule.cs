using Mono.Cecil;

namespace Store.Architecture.Tests;

internal static class PropertiesAreInitOnlyRule
{
    public static ConditionList HaveOnlyInitProperties(this Conditions conditions)
        => conditions.MeetCustomRule(MeetsRule);

    private static bool MeetsRule(TypeDefinition type)
        => type.Fields.All(field => field.IsInitOnly == true);
}