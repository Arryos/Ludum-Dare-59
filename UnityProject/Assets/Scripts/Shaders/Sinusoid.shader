Shader "Custom/Sinusoid"
{
    Properties
    {
        _Frequency("Frequency", Float) = 1.0
        _Thickness("Thickness", Float) = 0.01
        _Offset("Offset", Float) = 0.0
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #define PI 3.14159265359

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            CBUFFER_START(UnityPerMaterial)
                float _Frequency;
                float _Thickness;
                float _Offset;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Calculate the sine wave value at the current UV position
                // 2*PI is used to have one full wave cycle across the UV range of 0 to 1
                float frequncyMultiplier =_Frequency * 2 * PI;
                float x= IN.uv.x * frequncyMultiplier + _Offset;
                float amplitudeMultiplier = (1.0 - _Thickness) * 0.5;
                
                float sinX = 0.5 + amplitudeMultiplier * sin(x);
                
                float derivative = amplitudeMultiplier * frequncyMultiplier * cos(x);
                
                // Calculate the distance from the current UV position to the sine wave
                float dist= abs(IN.uv.y - sinX) / sqrt(1 + derivative * derivative);
                // Use smoothstep to create a smooth transition around the sine wave based on the thickness
                float lineAlpha = smoothstep(_Thickness/2 - fwidth(dist), _Thickness/2 + fwidth(dist), dist);
                                
                return float4(IN.color.rgb, 1-(lineAlpha * IN.color.a));
            }
            ENDHLSL
        }
    }
}
