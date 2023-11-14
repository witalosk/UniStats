using UnityEngine;

namespace UniStats
{
    public class DoubleGraphRenderer : GraphRendererBase, IDoubleGraphRenderer
    {
        public RenderTexture GetGraphTexture(IHistoryManager<float> historyManager1, Color color1, IHistoryManager<float> historyManager2, Color color2, float? graphMaxValue = null)
        {
            _graphMaterial.SetVector("_TexelSize", _texelSize);
            _graphMaterial.SetFloatArray("_Values1", historyManager1.GetLatestValues(_displayBufferLength, out float maxValue1));
            _graphMaterial.SetFloatArray("_Values2", historyManager2.GetLatestValues(_displayBufferLength, out float maxValue2));
            _graphMaterial.SetInt("_ValuesLength", _displayBufferLength);
            _graphMaterial.SetFloat("_GraphMaxValue", graphMaxValue ?? Mathf.Max(maxValue1, maxValue2));
            _graphMaterial.SetColor("_Color1", color1);
            _graphMaterial.SetColor("_Color2", color2);
            
            Graphics.Blit(null, _graphTexture, _graphMaterial);
            return _graphTexture;
        }
    }
}