namespace Store.Architecture.Tests;

internal static class ConditionsExtensions
{
    public static ConditionList ResideInFixedNamespace(this Conditions conditions, string @namespace)
        => conditions.ResideInNamespaceMatching($"^{@namespace}$");
}