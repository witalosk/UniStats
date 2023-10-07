namespace UniStats
{
    public interface IHistoryManager<out T>
    {
        T CurrentValue { get; }
        T AverageValue { get; }
        T MinValue { get; }
        T MaxValue { get; }
        T[] Values { get; }
        T[] GetLatestValues(int numValues);
    }
}