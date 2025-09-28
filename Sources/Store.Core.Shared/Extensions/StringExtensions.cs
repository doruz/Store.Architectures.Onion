namespace Store.Core.Shared;

public static class StringExtensions
{
    public static bool IsEqualTo(this string? actual, string? expected)
        => actual?.ToLower() == expected?.ToLower();
}

public static class NumericExtensions
{
    public static bool IsInRange(this int value, int min, int max)
        => value >= min && value <= max;

    public static bool IsNotInRange(this int value, int min, int max)
        => !value.IsInRange(min, max);
}