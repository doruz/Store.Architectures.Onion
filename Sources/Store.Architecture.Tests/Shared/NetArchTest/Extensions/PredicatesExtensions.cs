using Mono.Cecil;

namespace Store.Architecture.Tests;

internal static class PredicatesExtensions
{
    public static PredicateList MeetCustomRule(this Predicates conditions, Func<TypeDefinition, bool> typeCheck) 
        => conditions.MeetCustomRule(new CustomRule(typeCheck));
}