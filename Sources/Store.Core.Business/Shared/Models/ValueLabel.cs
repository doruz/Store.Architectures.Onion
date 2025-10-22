namespace Store.Core.Business.Shared;

public record ValueLabel<T>(T Value, string Label)
{
    public override string ToString() => Label;
}