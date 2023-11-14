using UnityEngine;
using UnityEngine.Profiling;

namespace UniStats
{
    public class MemoryDataProvider : MonoBehaviour, ITripleDataProvider
    {
        public float Element1Value => (Profiler.GetTotalAllocatedMemoryLong() >> 10) / 1024f;
        public float Element2Value => (Profiler.GetTotalReservedMemoryLong() >> 10) / 1024f;
        public float Element3Value => (Profiler.GetMonoUsedSizeLong() >> 10) / 1024f;
    }
}