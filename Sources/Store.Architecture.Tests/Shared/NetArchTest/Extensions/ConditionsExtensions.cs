using Mono.Cecil;

namespace Store.Architecture.Tests;

internal static class ConditionsExtensions
{
    public static ConditionList ResideInFixedNamespace(this Conditions conditions, string @namespace)
        => conditions.ResideInNamespaceMatching($"^{@namespace}$");

    public static ConditionList ImplementInterfaces(this Conditions conditions)
        => conditions.MeetCustomRule(type => type is { HasInterfaces: true, IsInterface: false });

    public static ConditionList HaveInitOnlyProperties(this Conditions conditions)
        => conditions.MeetCustomRule(type => type.Fields.All(field => field.IsInitOnly == true));

    public static ConditionList HaveDependencyOnPublicPropertiesFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new HaveDependencyOnPublicPropertiesFrom(@namespace));

    public static ConditionList HaveDependencyOnPublicMethodsFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new HaveDependencyOnPublicMethodsFrom(@namespace));

    public static ConditionList MeetCustomRule(this Conditions conditions, Func<TypeDefinition, bool> typeCheck)
        => conditions.MeetCustomRule(new GenericCustomRule(typeCheck));
}