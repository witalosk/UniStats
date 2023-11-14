using UnityEngine;

namespace UniStats
{
    public interface IGraphProvider
    {
        bool IsInited { get; }
        void Init(Vector2 uiSize);
    }
    
    public interface ISingleGraphRenderer : IGraphProvider
    {
        RenderTexture GetGraphTexture(IHistoryManager<float> historyManager, ColorConfig colorConfig, float? graphMaxValue = null);
    }
    
    public interface IDoubleGraphRenderer : IGraphProvider
    {
        RenderTexture GetGraphTexture(IHistoryManager<float> historyManager1, Color color1, IHistoryManager<float> historyManager2, Color color2, float? graphMaxValue = null);
    }
    
    public interface ITripleGraphRenderer : IGraphProvider
    {
        RenderTexture GetGraphTexture(
            IHistoryManager<float> historyManager1, Color color1, 
            IHistoryManager<float> historyManager2, Color color2,
            IHistoryManager<float> historyManager3, Color color3, 
            float? graphMaxValue = null
        );
    }
}