using UnityEngine;
using UnityEngine.Profiling;

namespace UniStats
{
    public class DefaultMemoryDataProvider : MonoBehaviour, IMemoryDataProvider
    {
        public long AllocatedMemory => Profiler.GetTotalAllocatedMemoryLong();
        public long ReservedMemory => Profiler.GetTotalReservedMemoryLong();
        public long MonoUsedSize => Profiler.GetMonoUsedSizeLong();
    }
}