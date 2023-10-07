using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniStats
{
    public class StatsViewer : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private VisualElement _viewerRoot;
        private IEnumerable<ModuleBase> _modules;
        
        private void OnEnable()
        {
            _uiDocument = GetComponent<UIDocument>();
            _viewerRoot = _uiDocument.rootVisualElement.Q<GroupBox>("StatsViewerRoot");
            _modules = GetComponentsInChildren<ModuleBase>();

            foreach (var module in _modules)
            {
                var element = module.ModuleTreeAsset.Instantiate();
                module.ModuleElementRoot = element;
                _viewerRoot.Add(element);
                module.Init();
            }
        }  
    }
}