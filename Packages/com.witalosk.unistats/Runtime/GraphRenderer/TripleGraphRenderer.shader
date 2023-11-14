Shader "Hidden/TripleGraphRenderer"
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
            #include "Common.hlsl"

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

            uniform float _Values1[128];
            uniform float _Values2[128];
            uniform float _Values3[128];
            int _ValuesLength;
            float _GraphMaxValue;

            half4 _Color1;
            half4 _Color2;
            half4 _Color3;
            
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
                const float val1 = _Values1[_ValuesLength - floor(i.uv.x * (float)_ValuesLength) - 1];
                const float val2 = _Values2[_ValuesLength - floor(i.uv.x * (float)_ValuesLength) - 1];
                const float val3 = _Values3[_ValuesLength - floor(i.uv.x * (float)_ValuesLength) - 1];
                
                // Graph1
                float diff = val1 - i.uv.y * _GraphMaxValue;
                float uvDiff = max(diff / val1, 0.0);
                half4 col1 = diff > 0.0 ? lerp(0.5, 1.0, 1.0 - uvDiff) * _Color1 : 0.0;
                col1 *= uvDiff < _TexelSize.x * DotSize ? 1.0 : 0.4;

                 // Graph2
                diff = val2 - i.uv.y * _GraphMaxValue;
                uvDiff = max(diff / val2, 0.0);
                half4 col2 = diff > 0.0 ? lerp(0.5, 1.0, 1.0 - uvDiff) * _Color2 : 0.0;
                col2 *= uvDiff < _TexelSize.x * DotSize ? 1.0 : 0.4;

                // Graph3
                diff = val3 - i.uv.y * _GraphMaxValue;
                uvDiff = max(diff / val3, 0.0);
                half4 col3 = diff > 0.0 ? lerp(0.5, 1.0, 1.0 - uvDiff) * _Color3 : 0.0;
                col3 *= uvDiff < _TexelSize.x * DotSize ? 1.0 : 0.4; 
                
                return BlendScreen(col1, BlendScreen(col2, col3));
            }
            ENDCG
        }
    }
}