using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public class FpsModule : ModuleBase
    {
        [SerializeField] private float _updateInterval = 0.5f;
        [SerializeField] private int _sampleNum = 512;
        [SerializeField] private int _latestValueSampleNum = 15;
        [SerializeField] private ColorConfig _colorConfig;
        
        private Label _fpsLabel;
        private Label _msecLabel;
        private Label _avgLabel;
        private Label _minLabel;
        private Label _maxLabel;
        private GroupBox _graphView;

        private float _timeSinceLastUpdate;
        private StyleBackground _graphBackground;
        
        private FloatHistoryManager _fpsFloatHistoryManager;
        private IFpsProvider _fpsProvider;
        private ISingleGraphRenderer _graphRenderer;
        
        public override void Init()
        {
            base.Init();
            
            _fpsFloatHistoryManager = new FloatHistoryManager(_sampleNum);
            
            _fpsLabel = ModuleElementRoot.Q<Label>("fpsVal");
            _msecLabel = ModuleElementRoot.Q<Label>("msecVal");
            _avgLabel = ModuleElementRoot.Q<Label>("avgVal");
            _minLabel = ModuleElementRoot.Q<Label>("minVal");
            _maxLabel = ModuleElementRoot.Q<Label>("maxVal");
            _graphView = ModuleElementRoot.Q<GroupBox>("GraphView");
            
            _fpsProvider = GetComponent<IFpsProvider>() ?? gameObject.AddComponent<DefaultFpsProvider>();
            _graphRenderer = GetComponent<ISingleGraphRenderer>();
            
            _graphView.RegisterCallback<GeometryChangedEvent>(_ =>
            {
                _graphRenderer.Init(new Vector2(_graphView.resolvedStyle.width, _graphView.resolvedStyle.height));
            });
            
            _graphBackground = new StyleBackground();
        }

        private void Update()
        {
            _fpsFloatHistoryManager.AddValue(_fpsProvider.Fps);
            _timeSinceLastUpdate += Time.unscaledDeltaTime;
            
            if (!_graphRenderer.IsInited) return;
            
            // Update Graph
            _graphBackground.value = Background.FromRenderTexture(_graphRenderer.GetGraphTexture(_fpsFloatHistoryManager, _colorConfig));
            _graphView.style.backgroundImage = _graphBackground;
            
            if (_timeSinceLastUpdate < _updateInterval) return;
            
            // Update UI
            float fps = _fpsFloatHistoryManager.GetLatestValues(_latestValueSampleNum).Average();
            _fpsLabel.text = $"{GetColoredFpsText(fps)} fps";
            _msecLabel.text = $"{1f / fps * 1000f:F2} ms";
            _avgLabel.text = GetColoredFpsText(_fpsFloatHistoryManager.AverageValue);
            _minLabel.text = GetColoredFpsText(_fpsFloatHistoryManager.MinValue);
            _maxLabel.text = GetColoredFpsText(_fpsFloatHistoryManager.MaxValue);
            
            _timeSinceLastUpdate = 0f;
        }

        private string GetColoredFpsText(float val)
        {
            if (val <= _colorConfig.LowThreshold) return $"<color=#{ColorUtility.ToHtmlStringRGB(_colorConfig.LowColor)}>{val:F0}</color>";
            if (val <= _colorConfig.HighThreshold) return $"<color=#{ColorUtility.ToHtmlStringRGB(_colorConfig.MiddleColor)}>{val:F0}</color>";
            
            return $"<color=#{ColorUtility.ToHtmlStringRGB(_colorConfig.HighColor)}>{val:F0}</color>";
        }

    }
}