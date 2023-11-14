using UnityEngine;

namespace UniStats
{
    public class DefaultGraphRenderer : GraphRendererBase, ISingleGraphRenderer
    {
        public RenderTexture GetGraphTexture(IHistoryManager<float> historyManager, ColorConfig colorConfig, float? graphMaxValue = null)
        {
            _graphMaterial.SetVector("_TexelSize", _texelSize);
            _graphMaterial.SetFloatArray("_Values", historyManager.GetLatestValues(_displayBufferLength));
            _graphMaterial.SetInt("_ValuesLength", _displayBufferLength);
            _graphMaterial.SetFloat("_AverageValue", historyManager.AverageValue);
            _graphMaterial.SetFloat("_GraphMaxValue", graphMaxValue ?? historyManager.MaxValue);
            _graphMaterial.SetFloat("_WarningThreshold", colorConfig.HighThreshold);
            _graphMaterial.SetFloat("_CriticalThreshold", colorConfig.LowThreshold);
            _graphMaterial.SetColor("_GoodColor", colorConfig.HighColor);
            _graphMaterial.SetColor("_WarningColor", colorConfig.MiddleColor);
            _graphMaterial.SetColor("_CriticalColor", colorConfig.LowColor);
            
            Graphics.Blit(null, _graphTexture, _graphMaterial);
            return _graphTexture;
        }
    }
}