using Mono.Cecil;

namespace Store.Tests;

internal static class PropertiesAreInitOnlyRule
{
    public static ConditionList HaveOnlyInitProperties(this Conditions conditions)
        => conditions.MeetCustomRule(AreAllPropertiesInitOnly);

    private static bool AreAllPropertiesInitOnly(TypeDefinition type)
        => type.Fields.All(field => field.IsInitOnly == true);
}