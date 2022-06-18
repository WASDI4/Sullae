Shader "Hidden/Outline_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            //이미 유니티에 있는 내부 변수
            sampler2D _CameraDepthNormalsTexture;
            float4 _CameraDepthNormalsTexture_TexelSize;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
                float3 normal;
                float depth;
                DecodeDepthNormal (depthNormal, depth, normal);
                
                //카메라의 far plane을 받아온다.
                depth = depth * _ProjectionParams.z;

                float4 neighborDepthNormal = tex2D(_CameraDepthNormalsTexture, 
                i.uv(_CameraDepthNormalsTexture_TexelSize.xy*float2(1,0)));


                
                float4 final;
                final.rgb = depth;
                final.a = 1;

                return final;
            }
            ENDCG
        }
    }
}
