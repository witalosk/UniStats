Shader "Hidden/DefaultGraphRenderer"
{
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            static const int DotSize = 4;

            float2 _TexelSize;

            uniform float _Values[128];
            int _ValuesLength;
            float _AverageValue;
            float _MinValue;
            float _MaxValue;

            float _WarningThreshold;
            float _CriticalThreshold;
            half4 _GoodColor;
            half4 _WarningColor;
            half4 _CriticalColor;

            inline half4 GetGraphColor(half4 color)
            {
                return color * color;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                const float val = _Values[_ValuesLength - floor(i.uv.x * (float)_ValuesLength) - 1];

                // Graph
                const float diff = val - i.uv.y * _MaxValue;
                const float uvDiff = max(diff / val, 0.0);
                half4 col = diff > 0.0 ? lerp(0.5, 1.0, 1.0 - uvDiff) : 0.0;
                col *= uvDiff < _TexelSize.x * DotSize ? 1.0 : 0.4; 
                
                // Graph Color
                col *= val < _CriticalThreshold ? GetGraphColor(_CriticalColor)
                    : val < _WarningThreshold ? GetGraphColor(_WarningColor)
                    : _GoodColor;
                
                // Threshold
                col += abs(i.uv.y - _CriticalThreshold / _MaxValue) < _TexelSize.y ? _WarningColor
                    : abs(i.uv.y - _WarningThreshold / _MaxValue) < _TexelSize.y ? _GoodColor
                    : col;
                
                return col;
            }
            ENDCG
        }
    }
}