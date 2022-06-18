Shader "OB_Image_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        //화면 전체에 적용되는 쉐이더. 앞뒤가 없고 z값이 기록되면 안됨.
        //z값이 항상 그려져야함.
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

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                //(r+g+b)/3을 하면 색상의 명도 값이 나옴. 
                //그것을 rgb에 모두 적용하면 그레이스케일이 적용된 화면이 나옴.
                float grayScale = (col.r+col.g+col.b)/3;

                float4 final;
                final.rgb = grayScale;
                final.a = 1;
                // just invert the colors
                //_MainTex를 불러와서 색 반전을 함.
                //col.rgb = 1 - col.rgb;
                return final;
            }
            ENDCG
        }
    }
}
