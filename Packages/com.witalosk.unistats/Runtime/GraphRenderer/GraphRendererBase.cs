using UnityEngine;

namespace UniStats
{
    public abstract class GraphRendererBase : MonoBehaviour, IGraphProvider
    {
        public bool IsInited { get; private set; } = false;
        
        [SerializeField] protected Material _graphMaterial;
        [SerializeField, Range(0f, 10f)] protected float _textureResolutionScale = 3f;
        
        protected RenderTexture _graphTexture;
        protected Vector2 _texelSize;
        
        protected const int _displayBufferLength = 128;

        public virtual void Init(Vector2 uiSize)
        {
            OnDestroy();
            
            _graphTexture = new RenderTexture((int)(_displayBufferLength * _textureResolutionScale), (int)(_displayBufferLength * _textureResolutionScale / uiSize.x * uiSize.y), 0, RenderTextureFormat.ARGB32)
            {
                filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp
            };
            _texelSize = new Vector2(1f / _graphTexture.width, 1f / _graphTexture.height);
            
            IsInited = true;
        }
        
        protected virtual void OnDestroy()
        {
            IsInited = false;
            
            if (_graphTexture != null)
            {
                _graphTexture.Release();
                _graphTexture = null;
            }
        }
    }
}