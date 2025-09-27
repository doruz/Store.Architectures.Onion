namespace Store.Core.Shared
{
    public record class ValueLabel<T>(T Value, string Label)
    {
        public override string ToString() => Label;
    }
}
