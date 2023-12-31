﻿Shader "Hidden/Toguchi/PostProcessing/Diffusion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"

    TEXTURE2D_X(_MainTex);
    float4 _MainTex_TexelSize;

    TEXTURE2D_X(_BlurTex);

    float _Contrast;
    float _Intensity;

    static const float Weights[9] = {
        0.5352615, 0.7035879, 0.8553453, 0.9616906, 1, 0.9616906, 0.8553453, 0.7035879, 0.5352615
    };

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
    }

    float3 Contrast(float3 In, float Contrast)
    {
        const float midpoint = pow(0.5, 2.2);
        return (In - midpoint) * Contrast + midpoint;
    }

    half4 Frag_Contrast(v2f input) : SV_Target
    {
        half4 color = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, input.uv);

        color.rgb = Contrast(color.rgb, _Contrast);

        return color;
    }

    half4 Frag_Blur1(v2f input) : SV_Target
    {
        half4 color = 0;

        float totalWeight = 0;

        for (int i = -4; i <= 4; i++)
        {
            float weight = Weights[i + 4];
            totalWeight += weight;
            color += SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp,
                                                        input.uv + float4(i * _MainTex_TexelSize.x, 0, 0, 0)) * weight;
        }

        color /= totalWeight;

        return color;
    }

    half4 Frag_Blur2(v2f input) : SV_Target
    {
        half4 color = 0;

        float totalWeight = 0;

        for (int i = -4; i <= 4; i++)
        {
            float weight = Weights[i + 4];
            totalWeight += weight;
            color += SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp,
                input.uv + float4(0, i * _MainTex_TexelSize.y, 0, 0)) * weight;
        }

        color /= totalWeight;

        return color;
    }

    half4 Frag_Blend(v2f input) : SV_Target
    {
        half4 color = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, input.uv);
        half4 blur = SAMPLE_TEXTURE2D_X(_BlurTex, sampler_LinearClamp, input.uv);

        color.rgb = 1.0 - (1.0 - color.rgb) * (1.0 - blur.rgb * _Intensity);
        // color.rgb = lerp(color.rgb, blur.rgb, _Intensity);

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

        Pass // 0
        {
            Name "Contrast"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment Frag_Contrast
            ENDHLSL
        }

        Pass // 1
        {
            Name "Blur1"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment Frag_Blur1
            ENDHLSL
        }

        Pass // 2
        {
            Name "Blur2"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment Frag_Blur2
            ENDHLSL
        }

        Pass // 3
        {
            Name "Blend"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment Frag_Blend
            ENDHLSL
        }
    }
}