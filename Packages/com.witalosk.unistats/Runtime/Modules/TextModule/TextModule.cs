using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public class TextModule : ModuleBase
    {
        [SerializeField] private int _fontSize = 7;
        
        private Label _label;
        private ITextProvider _textProvider;

        public override void Init()
        {
            base.Init();

            _textProvider = GetComponent<ITextProvider>();
            _textProvider.Init();
            
            _label = ModuleElementRoot.Q<Label>("Text");
            _label.text = _textProvider.Text;
        }

        private void Update()
        {
            _label.text = _textProvider.Text;
            _label.style.fontSize = _fontSize;
        }
    }
}