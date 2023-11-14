using UnityEngine;

namespace UniStats
{
    public class TripleGraphRenderer : GraphRendererBase, ITripleGraphRenderer
    {
        public RenderTexture GetGraphTexture(IHistoryManager<float> historyManager1, Color color1,
            IHistoryManager<float> historyManager2, Color color2,
            IHistoryManager<float> historyManager3, Color color3, float? graphMaxValue = null)
        {
            _graphMaterial.SetVector("_TexelSize", _texelSize);
            _graphMaterial.SetFloatArray("_Values1", historyManager1.GetLatestValues(_displayBufferLength, out float maxValue1));
            _graphMaterial.SetFloatArray("_Values2", historyManager2.GetLatestValues(_displayBufferLength, out float maxValue2));
            _graphMaterial.SetFloatArray("_Values3", historyManager3.GetLatestValues(_displayBufferLength, out float maxValue3));
            _graphMaterial.SetInt("_ValuesLength", _displayBufferLength);
            _graphMaterial.SetFloat("_GraphMaxValue", graphMaxValue ?? Mathf.Max(maxValue1, maxValue2, maxValue3));
            _graphMaterial.SetColor("_Color1", color1);
            _graphMaterial.SetColor("_Color2", color2);
            _graphMaterial.SetColor("_Color3", color3);
            
            Graphics.Blit(null, _graphTexture, _graphMaterial);
            return _graphTexture;
        }
    }
}