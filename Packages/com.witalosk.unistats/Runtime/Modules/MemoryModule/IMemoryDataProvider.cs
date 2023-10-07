namespace UniStats
{
    public interface IMemoryDataProvider
    {
        long AllocatedMemory { get; }
        long ReservedMemory { get; }
        long MonoUsedSize { get; }
    }
}