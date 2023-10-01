Shader "Hidden/Toguchi/PostProcessing/GradientFog"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"

    TEXTURE2D_X(_MainTex);
    TEXTURE2D_X(_CameraDepthTexture);

    TEXTURE2D_X(_RampTex);
    float _Intensity;

    struct appdata
    {
        float4 positionOS : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float4 positionCS : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    v2f vert(appdata input)
    {
        v2f output;
        output.positionCS = TransformObjectToHClip(input.positionOS);
        output.uv = input.uv;
        return output;
        UNITY_VERTEX_OUTPUT_STEREO
    }

    half4 Frag(v2f input) : SV_Target
    {
        half4 color = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, input.uv);
        float depth = SAMPLE_TEXTURE2D_X(_CameraDepthTexture, sampler_LinearClamp, input.uv).r;
        depth = Linear01Depth(depth, _ZBufferParams);

        half4 ramp = SAMPLE_TEXTURE2D_X(_RampTex, sampler_LinearClamp, float2(depth, 0));
        color.rgb = lerp(color.rgb, ramp.rgb, _Intensity * ramp.a);

        return color;
    }
    ENDHLSL

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100
        ZTest Always ZWrite Off Cull Off

        Pass
        {
            Name "GradientFog"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}