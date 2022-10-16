namespace Genometric.MSPC.Core.Model
{
    public enum Attributes : byte
    {
        Background = 2,
        Weak = 4,
        Stringent = 8,
        Confirmed = 16,
        Discarded = 32,
        TruePositive = 64,
        FalsePositive = 128
    };

    public enum MultipleIntersections : byte
    {
        UseLowestPValue = 0,
        UseHighestPValue
    };

    public enum ReplicateType : byte
    {
        Technical = 0,
        Biological = 1
    };
}
