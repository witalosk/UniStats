using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public class MemoryModule : ModuleBase
    {
        [SerializeField] private float _updateInterval = 0.5f;
        [SerializeField] private int _sampleNum = 128;
        [SerializeField] private Color _reservedColor = new(0f, 0.7294f, 0.7686f);
        [SerializeField] private Color _allocatedColor = new(0.4549f, 0.8980f, 0.7802f);
        [SerializeField] private Color _monoHeapColor = new(0.7634f, 0.71f, 1f);
        
        private Label _allocatedLabel;
        private Label _reservedLabel;
        private Label _monoHeapLabel;
        private GroupBox _graphView;
        
        private float _timeSinceLastUpdate;
        private StyleBackground _graphBackground;
        
        private FloatHistoryManager _allocatedHistoryManager;
        private FloatHistoryManager _reservedHistoryManager;
        private FloatHistoryManager _monoHeapHistoryManager;
        private IMemoryDataProvider _memoryDataProvider;
        private IDoubleGraphRenderer _graphRenderer;
        
        public override void Init()
        {
            base.Init();
            
            _allocatedHistoryManager = new FloatHistoryManager(_sampleNum);
            _reservedHistoryManager = new FloatHistoryManager(_sampleNum);
            _monoHeapHistoryManager = new FloatHistoryManager(_sampleNum);
            
            _allocatedLabel = ModuleElementRoot.Q<Label>("allocatedVal");
            _reservedLabel = ModuleElementRoot.Q<Label>("reservedVal");
            _monoHeapLabel = ModuleElementRoot.Q<Label>("monoVal");
            _graphView = ModuleElementRoot.Q<GroupBox>("GraphView");
            
            _memoryDataProvider = GetComponent<IMemoryDataProvider>() ?? gameObject.AddComponent<DefaultMemoryDataProvider>();
            _graphRenderer = GetComponent<IDoubleGraphRenderer>();
            
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

            _allocatedHistoryManager.AddValue((_memoryDataProvider.AllocatedMemory >> 10) / 1024f);
            _reservedHistoryManager.AddValue((_memoryDataProvider.ReservedMemory >> 10) / 1024f);
            _monoHeapHistoryManager.AddValue((_memoryDataProvider.MonoUsedSize >> 10) / 1024f);
            
            // Update Graph
            _graphBackground.value = Background.FromRenderTexture(_graphRenderer.GetGraphTexture(_reservedHistoryManager, _reservedColor, _allocatedHistoryManager, _allocatedColor));
            _graphView.style.backgroundImage = _graphBackground;
            
            // Update UI
            _allocatedLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_allocatedColor)}>{_allocatedHistoryManager.CurrentValue:F0}</color> MB";
            _reservedLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_reservedColor)}>{_reservedHistoryManager.CurrentValue:F0}</color> MB";
            _monoHeapLabel.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_monoHeapColor)}>{_monoHeapHistoryManager.CurrentValue:F0}</color> MB";
            
            _timeSinceLastUpdate = 0f;
        }
    }
}