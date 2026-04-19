Shader "Custom/VertexSquare"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        _Amplitude("Amplitude", Float) = 1.0
        _Frequency("Frequency", Float) = 1.0
        _Offset("Offset", Float) = 0.0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                float _Amplitude;
                float _Frequency;
                float _Offset;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float4 pos = IN.positionOS;

                float sign = 1;
                if (sin(_Frequency * pos.x * 2 * PI + _Offset) < 0)
                {
                    sign = -1;
                }
                pos.z += sign * _Amplitude;

                OUT.positionHCS = TransformObjectToHClip(pos.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;
                return color;
            }
            ENDHLSL
        }
    }
}