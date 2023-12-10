Shader "Custom/FakeCloudShadowShader"
{
    Properties
    {
        _Speed("Speed", Vector) = (0.1, 0, 0.1, 0)
        _MainTex ("Cloud Texture 1", 2D) = "white" {}
        _MainTex2 ("Cloud Texture 2", 2D) = "white" {}
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting Off
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {

            Name "CloudShadow"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _Speed;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _MainTex2;
            float4 _MainTex2_ST;

            fixed4 frag(v2f_init_customrendertexture i) : SV_Target
            {
                float4 cloud = tex2D(_MainTex, i.texcoord.xy + frac(_Time * _Speed.xy));
                float4 cloud2 = tex2D(_MainTex2, i.texcoord.xy + frac(_Time * _Speed.zw));
                return max(0.1f, cloud * cloud2);
            }
            ENDCG
        }
    }
}