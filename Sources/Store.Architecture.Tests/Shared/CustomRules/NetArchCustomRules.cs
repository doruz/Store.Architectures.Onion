using Mono.Cecil;
using Mono.Cecil.Rocks;
using Store.Core.Domain.Entities;

namespace Store.Architecture.Tests;

internal static partial class NetArchCustomRules
{
    public static ConditionList ImplementInterfaces(this Conditions conditions)
        => conditions.MeetCustomRule(type => type is { HasInterfaces: true, IsInterface: false });

    public static ConditionList HaveInitOnlyProperties(this Conditions conditions)
        => conditions.MeetCustomRule(type => type.Fields.All(field => field.IsInitOnly == true));

    public static ConditionList HaveDependencyOnPublicPropertiesFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new HaveDependencyOnPublicPropertiesFrom(@namespace));

    public static ConditionList HaveDependencyOnPublicMethodsFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new HaveDependencyOnPublicMethodsFrom(@namespace));

    public static ConditionList MeetCustomRule(this Conditions conditions, Func<TypeDefinition, bool> typeCheck)
    {
        return conditions.MeetCustomRule(new CustomRule(typeCheck));
    }

    internal class CustomRule(Func<TypeDefinition, bool> typeCheck) : ICustomRule
    {
        public bool MeetsRule(TypeDefinition type) => typeCheck(type);

        public static implicit operator CustomRule(Func<TypeDefinition, bool> typeCheck) => new (typeCheck);
    }
}