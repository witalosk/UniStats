using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public abstract class ModuleBase : MonoBehaviour
    {
        public VisualTreeAsset ModuleTreeAsset => _moduleTreeAsset;
        public VisualElement ModuleElementRoot { get; set; }

        [SerializeField] protected VisualTreeAsset _moduleTreeAsset;
        [SerializeField] protected string _moduleName;

        public virtual void Init()
        {
            if (string.IsNullOrEmpty(_moduleName))
            {
                _moduleName = GetType().Name;
            }
            
            ModuleElementRoot.Q<Label>("ModuleLabel").text = _moduleName;
        }
    }
}