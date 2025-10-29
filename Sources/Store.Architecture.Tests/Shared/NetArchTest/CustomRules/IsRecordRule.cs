using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Architecture.Tests;

internal static class IsRecordRule
{
    public static ConditionList IsRecord(this Conditions conditions)
        => conditions.MeetCustomRule(CheckType);

    public static PredicateList AreRecords(this Predicates predicates)
        => predicates.MeetCustomRule(CheckType);

    private static bool CheckType(TypeDefinition type)
        => type.GetMethods().Any(m => m.Name == "<Clone>$");
}