using Unity.Profiling;
using UnityEngine;

namespace UniStats
{
    public class RenderingDataProvider : MonoBehaviour, ITripleDataProvider
    {
        public float Element1Value => _drawCallsRecorder.LastValue;
        public float Element2Value => _setPassCallsRecorder.LastValue;
        public float Element3Value => _verticesRecorder.LastValue / _verticesDivider;
        
        [SerializeField]
        private float _verticesDivider = 1000f;
        
        private ProfilerRecorder _drawCallsRecorder;
        private ProfilerRecorder _setPassCallsRecorder;
        private ProfilerRecorder _verticesRecorder;

        private void Start()
        {
            _drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            _setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            _verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }

        private void OnDestroy()
        {
            _drawCallsRecorder.Dispose();
            _setPassCallsRecorder.Dispose();
            _verticesRecorder.Dispose();
        }
    }
}