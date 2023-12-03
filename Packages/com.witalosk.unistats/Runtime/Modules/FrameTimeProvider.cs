using System;
using Unity.Profiling;
using UnityEngine;

namespace UniStats
{
    public class FrameTimeProvider : MonoBehaviour, ITripleDataProvider
    {
        public float Element1Value => _totalCpuFrameTimeRecorder.LastValue / 1000000f;
        public float Element2Value => _mainThreadFrameTimeRecorder.LastValue / 1000000f;
        public float Element3Value => _gpuFrameTimeRecorder.LastValue / 1000000f;
        
        private ProfilerRecorder _totalCpuFrameTimeRecorder;
        private ProfilerRecorder _mainThreadFrameTimeRecorder;
        private ProfilerRecorder _gpuFrameTimeRecorder;

        private void Start()
        {
            _totalCpuFrameTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "CPU Total Frame Time");
            _mainThreadFrameTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "CPU Main Thread Frame Time");
            _gpuFrameTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "GPU Frame Time");
        }
        
        private void OnDestroy()
        {
            _totalCpuFrameTimeRecorder.Dispose();
            _mainThreadFrameTimeRecorder.Dispose();
            _gpuFrameTimeRecorder.Dispose();
        }
    }
}