using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public class TripleValueModule : ModuleBase
    {
        [SerializeField] private float _updateInterval = 0.5f;
        [SerializeField] private int _sampleNum = 128;

        [SerializeField] private ElementSettings _element1 = new("Allocated", "MB", new Color(0f, 0.7294f, 0.7686f));
        [SerializeField] private ElementSettings _element2 = new("Reserved", "MB", new Color(0.4549f, 0.8980f, 0.7802f));
        [SerializeField] private ElementSettings _element3 = new("Mono", "MB", new(0.7634f, 0.71f, 1f));
        
        private Label _element1ValLabel;
        private Label _element2ValLabel;
        private Label _element3ValLabel;
        private GroupBox _graphView;
        
        private float _timeSinceLastUpdate;
        private StyleBackground _graphBackground;
        
        private FloatHistoryManager _element1HistoryManager;
        private FloatHistoryManager _element2HistoryManager;
        private FloatHistoryManager _element3HistoryManager;
        private ITripleDataProvider _tripleDataProvider;
        private ITripleGraphRenderer _graphRenderer;
        
        public override void Init()
        {
            base.Init();
            
            _element1HistoryManager = new FloatHistoryManager(_sampleNum);
            _element2HistoryManager = new FloatHistoryManager(_sampleNum);
            _element3HistoryManager = new FloatHistoryManager(_sampleNum);

            ModuleElementRoot.Q<Label>("element1Label").text = _element1.Label;
            ModuleElementRoot.Q<Label>("element2Label").text = _element2.Label;
            ModuleElementRoot.Q<Label>("element3Label").text = _element3.Label;
            _element1ValLabel = ModuleElementRoot.Q<Label>("element1Val");
            _element2ValLabel = ModuleElementRoot.Q<Label>("element2Val");
            _element3ValLabel = ModuleElementRoot.Q<Label>("element3Val");
            _graphView = ModuleElementRoot.Q<GroupBox>("GraphView");
            
            _tripleDataProvider = GetComponent<ITripleDataProvider>() ?? gameObject.AddComponent<MemoryDataProvider>();
            _graphRenderer = GetComponent<ITripleGraphRenderer>();
            
            _graphView.RegisterCallback<GeometryChangedEvent>(_ =>
            {
                _graphRenderer.Init(new Vector2(_graphView.resolvedStyle.width, _graphView.resolvedStyle.height));
            });
            
            _graphBackground = new StyleBackground();
        }

        private void Update()
        {
            _timeSinceLastUpdate += Time.unscaledDeltaTime;

            if (!_graphRenderer.IsInited || _timeSinceLastUpdate < _updateInterval) return;

            _element1HistoryManager.AddValue(_tripleDataProvider.Element1Value);
            _element2HistoryManager.AddValue(_tripleDataProvider.Element2Value);
            _element3HistoryManager.AddValue(_tripleDataProvider.Element3Value);
            
            // Update Graph
            _graphBackground.value = Background.FromRenderTexture(_graphRenderer.GetGraphTexture(
                _element1HistoryManager, _element1.Color,
                _element2HistoryManager, _element2.Color,
                _element3HistoryManager, _element3.Color
            ));
            _graphView.style.backgroundImage = _graphBackground;
            
            // Update UI
            _element1ValLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_element1.Color)}>{_element1HistoryManager.CurrentValue:F0}</color> {_element1.Unit}";
            _element2ValLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_element2.Color)}>{_element2HistoryManager.CurrentValue:F0}</color> {_element2.Unit}";
            _element3ValLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_element3.Color)}>{_element3HistoryManager.CurrentValue:F0}</color> {_element3.Unit}";
            
            _timeSinceLastUpdate = 0f;
        }
    }

    [Serializable]
    public class ElementSettings
    {
        public string Label;
        public string Unit;
        public Color Color;

        public ElementSettings(string label, string unit, Color col)
        {
            Label = label;
            Unit = unit;
            Color = col;
        }
    }
}