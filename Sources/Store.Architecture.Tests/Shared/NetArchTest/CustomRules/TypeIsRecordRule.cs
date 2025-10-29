using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Architecture.Tests;

internal static class TypeIsRecordRule
{
    public static ConditionList IsRecord(this Conditions conditions)
        => conditions.MeetCustomRule(MeetsRule);

    public static PredicateList AreRecords(this Predicates predicates)
        => predicates.MeetCustomRule(MeetsRule);

    private static bool MeetsRule(TypeDefinition type)
        => type.GetMethods().Any(m => m.Name == "<Clone>$");
}